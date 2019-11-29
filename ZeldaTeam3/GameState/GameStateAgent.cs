using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Dungeon;
using Zelda.HUD;
using Zelda.Music;
using Zelda.Player;
using Zelda.ShaderEffects;

namespace Zelda.GameState
{
    /*
     * Handles game state transitions and screen panning
     */
    public class GameStateAgent : IGameStateAgent
    {
        private const float Scale = 2.0f;

        public DungeonManager DungeonManager { get; } = new DungeonManager();
        public IDrawable HUD { get; }
        public IPlayer Player { get; private set; } = new Link(Point.Zero);
        public bool Quitting { get; private set; }

        private bool _darkMode;
        public bool DarkMode {
            private get
            {
                return _darkMode && _worldState == WorldState.Playing;
            }
            set { _darkMode = value; }
        }

        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private PauseTransitionStateMachine _pauseMachine = new PauseTransitionStateMachine();
        private IDrawable _sourceScene;
        private IDrawable _destinationScene;
        private Point _panDestination;
        private PanAnimation _panAnimation;

        private WorldState _worldState = WorldState.Playing;
        private GameWorld _world;

        private readonly LightInTheDarkness _lightInTheDarkness = new LightInTheDarkness();
        private readonly PartyTime _partyTime = new PartyTime();
        private bool _partyHard;

        public GameStateAgent(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            HUD = new HUDScreen(this, new Point(0, -HUDSpriteFactory.ScreenHeight));
            DungeonManager.Pan = DungeonPan;
        }

        private float YOffset => HUDSpriteFactory.ScreenHeight + _pauseMachine.YOffset;

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

        public void JumpMap()
        {
            if (_worldState == WorldState.JumpMap) return;
            _world = new JumpMapWorld(this);
            _worldState = WorldState.JumpMap;
        }

        public void GameOver()
        {
            if (_worldState == WorldState.GameOver) return;
            _world = new GameOverWorld(this);
            _worldState = WorldState.GameOver;
        }

        public void GameWin()
        {
            if (_worldState == WorldState.GameWin) return;
            _world = new GameWinWorld(this);
            _worldState = WorldState.GameWin;
        }

        public void JumpToRoom(int row, int column)
        {
            DungeonManager.JumpToRoom(row, column);
            Play();
        }

        public void DungeonPan(Point sourceRoom, Point destinationRoom, Direction direction)
        {
            if (DarkMode)
            {
                DungeonManager.JumpToRoom(destinationRoom.Y, destinationRoom.X, direction);
                return;
            }

            if (_worldState == WorldState.DungeonPanning) return;
            _panAnimation = new PanAnimation(direction);
            _sourceScene = DungeonManager.BuildPanScene(sourceRoom.Y, sourceRoom.X);
            _destinationScene = DungeonManager.BuildPanScene(destinationRoom.Y, destinationRoom.X);
            _panDestination = destinationRoom;
            _world = new PanningWorld(this);
            _worldState = WorldState.DungeonPanning;
        }

        public void Continue()
        {
            DungeonManager.ResetScenes();
            Player.Spawn();
            DungeonManager.JumpToRoom(5, 2);
            Play();
        }

        public void Reset()
        {
            if (_worldState == WorldState.Reset) return;
            _worldState = WorldState.Reset;

            Player = new Link(Point.Zero);
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
            _partyTime.Update();

            if (_world == null) return;

            if (_panAnimation != null)
            {
                if (_panAnimation.Finished)
                {
                    DungeonManager.JumpToRoom(_panDestination.Y, _panDestination.X, _panAnimation.Direction);
                    Play();
                    _panAnimation = null;
                }
                else
                {
                    _panAnimation.Update();
                }
            }

            foreach (var updatable in _world.Updatables)
            {
                updatable.Update();
            }

            _lightInTheDarkness.Track(Player.Location.ToVector2() + new Vector2(8.0f, YOffset + 8.0f), Player.Direction);

            if (!Player.Alive) GameOver();
            if (Player.Won) GameWin();
            if (_pauseMachine.State != PauseState.Unpaused) return;

            _pauseMachine.Play();
            Play();
        }

        private void DrawPan()
        {
            const int yOffset = HUDSpriteFactory.ScreenHeight;

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null,
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation(_panAnimation.SourceOffset.X * Scale,
                    (_panAnimation.SourceOffset.Y + yOffset) * Scale, 0.0f));
            if (_partyHard) _partyTime.Apply();
            _sourceScene.Draw();
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null,
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation(_panAnimation.DestinationOffset.X * Scale,
                    (_panAnimation.DestinationOffset.Y + yOffset) * Scale, 0.0f));
            if (_partyHard) _partyTime.Apply();
            _destinationScene.Draw();
            _spriteBatch.End();
        }

        private void Render()
        {
            if (_world == null) return;

            if (_worldState == WorldState.DungeonPanning) DrawPan();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Scale) * Matrix.CreateTranslation(0.0f, YOffset * Scale, 0.0f));
            if (_partyHard) _partyTime.Apply();
            foreach (var drawable in _world.ScaledDrawables)
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

        public void Draw()
        {
            Texture2D overlay = null;
            if (DarkMode) overlay = _lightInTheDarkness.Overlay(Render, new Vector2(0, YOffset * Scale));

            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.Black);

            Render();


            if (!DarkMode) return;
            _spriteBatch.Begin(SpriteSortMode.Immediate);
            _spriteBatch.Draw(overlay, Vector2.Zero, Color.Black);
            _spriteBatch.End();
        }

        public void PartyHard()
        {
            _partyHard = true;
            Sprite.PartyHard = true;
            Player.PartyHard();
        }
    }
}
