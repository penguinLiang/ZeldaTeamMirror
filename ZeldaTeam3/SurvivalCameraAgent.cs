using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.HUD;

namespace Zelda
{
    /*
     * Handles game state transitions and screen panning
     */
    public class SurvivalCameraAgent : IDrawable
    {
        public const float Scale = 2.0f;

        //public DungeonManager DungeonManager { get; } = new DungeonManager();
        //public HUDScreen HUD { get; }
        //public IPlayer Player { get; private set; }

        private readonly SpriteBatch _spriteBatch;
        private IPlayer _player;
        private IDrawable _scene;

        public SurvivalCameraAgent(SpriteBatch spriteBatch, IPlayer player, IDrawable scene)
        {
            _spriteBatch = spriteBatch;
            _player = player;
            _scene = scene;
        }

        public void ChangePlayer(IPlayer player)
        {
            _player = player;
        }

        /*
        public void DungeonPan(Point sourceRoom, Point destinationRoom, Direction direction)
        {
            if (_worldState == WorldState.DungeonPanning) return;
            _panAnimation = new PanAnimation(direction);
            _sourceScene = DungeonManager.BuildPanScene(sourceRoom.Y, sourceRoom.X);
            _destinationScene = DungeonManager.BuildPanScene(destinationRoom.Y, destinationRoom.X);
            _panDestination = destinationRoom;
            _world = new PanningWorld(this);
            _worldState = WorldState.DungeonPanning;
        }
        */
        public void Update()
        {


            /*_pauseMachine.Update();

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

            if (!Player.Alive) GameOver();
            if (Player.Won) GameWin();
            if (_pauseMachine.State != PauseState.Unpaused) return;

            _pauseMachine.Play();
            Play(); */
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                Matrix.CreateScale(Scale) * Matrix.CreateTranslation((_player.Location.X + 8) * Scale,
                    (_player.Location.Y + HUDSpriteFactory.ScreenHeight + 8) * Scale, 0.0f));
            _scene.Draw();
            _spriteBatch.End();
        }
    }
}
