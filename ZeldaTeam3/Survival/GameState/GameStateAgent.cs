using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Dungeon;
using Zelda.Music;
using Zelda.Player;
using Zelda.Survival.HUD;

namespace Zelda.Survival.GameState
{
    /*
     * Handles game state transitions and screen panning
     */
    public class GameStateAgent : IGameStateAgent
    {
        public const float Scale = 2.0f;

        public DungeonManager DungeonManager { get; } = new SurvivalManager();
        public IDrawable HUD { get; }
        public IPlayer Player { get; private set; } = new Link(Point.Zero);
        public bool Quitting { get; private set; }

        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private PauseTransitionStateMachine _pauseMachine = new PauseTransitionStateMachine();
        private PlayerLockCamera _camera;

        private WorldState _worldState = WorldState.Playing;
        private GameWorld _world;

        public GameStateAgent(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            HUD = new HUDScreen(this, new Point(0, -HUDSpriteFactory.ScreenHeight));
            DungeonManager.Pan = DungeonPan;
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

        public void Scoreboard()
        {
            if (_worldState == WorldState.Scoreboard) return;
            _world = new ScoreboardWorld(this);
            _worldState = WorldState.Scoreboard;
        }

        public void GameWin()
        {
            if (_worldState == WorldState.GameWin) return;
            _world = new GameWinWorld(this);
            _worldState = WorldState.GameWin;
        }

        public void DungeonPan(Point sourceRoom, Point destinationRoom, Direction direction)
        {
            DungeonManager.JumpToRoom(destinationRoom.Y, destinationRoom.X, direction);
            Play();
        }

        public void Continue()
        {
            DungeonManager.ResetScenes();
            Player.Spawn();
            DungeonManager.JumpToRoom(1, 0);
            Play();
        }

        public void Reset()
        {
            if (_worldState == WorldState.Reset) return;
            _worldState = WorldState.Reset;

            Player = new Link(Point.Zero);
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

            if (_world == null) return;

            foreach (var updatable in _world.Updatables)
            {
                updatable.Update();
            }

            if (!Player.Alive) GameOver();
            if (Player.Won) GameWin();
            if (_pauseMachine.State != PauseState.Unpaused) return;

            _pauseMachine.Play();
            Play();
        }

        public void Draw()
        {
            if (_world == null) return;
            _graphicsDevice.Clear(Color.Black);

            // DrawFollow(); // UNCOMMENT THIS LINE TO TEST SURVIVAL CAMERA

            // COMMENT OUT THE REMAINING CODE FOR THIS METHOD TO TEST SURVIVAL CAMERA

            var yOffset = HUDSpriteFactory.ScreenHeight + _pauseMachine.YOffset;

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation(_camera.X * Scale, (_camera.Y + yOffset) * Scale, 0.0f));
            foreach (var drawable in _world.CameraDrawables)
            {
                drawable.Draw();
            }
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Scale) * Matrix.CreateTranslation(0.0f, yOffset * Scale, 0.0f));
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
    }
}
