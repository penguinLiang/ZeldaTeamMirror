using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Music;
using Zelda.Player;
using Zelda.ShaderEffects;
using Zelda.Survival.HUD;

namespace Zelda.Survival.GameState
{
    /*
     * Handles game state transitions and screen panning
     */
    public class GameStateAgent : IGameStateAgent
    {
        public const float Scale = 2.0f;

        private readonly SurvivalManager _survivalManager = new SurvivalManager();
        public IDungeonManager DungeonManager => _survivalManager;
        public IDrawable HUD { get; }
        public IPlayer Player { get; private set; } = new Link(Point.Zero);
        public bool Quitting { get; private set; }

        public int Score => _survivalManager.CurrentWave;

        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private PauseTransitionStateMachine _pauseMachine = new PauseTransitionStateMachine();
        private PlayerLockCamera _camera;

        private WorldState _worldState = WorldState.Playing;
        private GameWorld _world;

        private readonly PartyTime _partyTime = new PartyTime();

        public GameStateAgent(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            HUD = new HUDScreen(this, new Point(0, -HUDSpriteFactory.ScreenHeight));
            _camera = new PlayerLockCamera(Player);
        }

        public void Play()
        {
            _world = new PlayingWorld(this);
            _worldState = WorldState.Playing;
        }

        public void Resume()
        {
            if (_pauseMachine.State != PauseState.Paused) return;
            _pauseMachine.Resume();
        }

        public void Pause()
        {
            if (_worldState != WorldState.Playing || _pauseMachine.State != PauseState.Playing) return;
            _pauseMachine.Pause();
            _world = new PausedWorld(this);
            _worldState = WorldState.Playing;
        }

        public void GameOver()
        {
            if (_worldState == WorldState.GameOver) return;
            _world = new GameOverWorld(this);
            _worldState = WorldState.GameOver;
        }

        public void Continue()
        {
            DungeonManager.ResetScenes();
            Player.Spawn();
            Player.MaxHealth = 12;
            Player.FullHeal();
            DungeonManager.JumpToRoom(0, 0);
            Play();
        }

        public void Reset()
        {
            if (_worldState == WorldState.Reset) return;
            _worldState = WorldState.Reset;

            Player = new Link(Point.Zero);
            Player.Inventory.RupeeCount = 50;
            _camera = new PlayerLockCamera(Player);
            _pauseMachine = new PauseTransitionStateMachine();
            MusicManager.Instance.StopMusic();
            DungeonManager.LoadScenes(Player);
            Continue();
        }

        public void Quit()
        {
            Quitting = true;
        }

        public void Update()
        {
            _pauseMachine.Update();
            Sprite.PartyHard = _survivalManager.PartyHard;
            if (_survivalManager.PartyHard) _partyTime.Update();

            if (_world == null) return;

            foreach (var updatable in _world.Updatables)
            {
                updatable.Update();
            }


            if (_worldState == WorldState.Scoreboard) return;
            if (!Player.Alive) GameOver();
            if (_pauseMachine.State != PauseState.Unpaused) return;

            _pauseMachine.Play();
            Play();
        }

        public void Draw()
        {
            if (_world == null) return;
            _graphicsDevice.Clear(Color.Black);

            var yOffset = HUDSpriteFactory.ScreenHeight + _pauseMachine.YOffset;

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null,
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation(_camera.X * Scale, (_camera.Y + yOffset) * Scale, 0.0f));
            if (_survivalManager.PartyHard) _partyTime.Apply();
            foreach (var drawable in _world.CameraDrawables)
            {
                drawable.Draw();
            }
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Scale) * Matrix.CreateTranslation(0.0f, yOffset * Scale, 0.0f));
            if (_survivalManager.PartyHard) _partyTime.Apply();
            foreach (var drawable in _world.FixedDrawables)
            {
                drawable.Draw();
            }
            _spriteBatch.End();

            _spriteBatch.Begin();
            foreach (var drawable in _world.UnscaledDrawables)
            {
                drawable.Draw();
            }
            _spriteBatch.End();
        }

        public void SubmitScore()
        {
            if (_worldState == WorldState.Scoreboard) return;
            _world = new SubmitScoreWorld(this);
            _worldState = WorldState.Scoreboard;
        }
    }
}
