using Zelda.Survival.GameOver;
using Zelda.Music;

namespace Zelda.Survival.GameState
{
    internal class GameOverWorld : GameWorld, IUpdatable
    {
        private IUpdatable[] _updatables;
        private IDrawable[] _fixedDrawables = {};
        private IDrawable[] _cameraDrawables;
        public override IUpdatable[] Updatables => _updatables;
        public override IDrawable[] CameraDrawables => _cameraDrawables;
        public override IDrawable[] FixedDrawables => _fixedDrawables;

        private readonly GameOverMenu _screen;
        private readonly FrameDelay _menuDelay = new FrameDelay(300);
        private readonly GameOverControllerKeyboard _controllerKeyboard;

        public GameOverWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayGameOverMusic();
            _screen = new GameOverMenu(agent);
            _controllerKeyboard = new GameOverControllerKeyboard(agent, _screen);
            _updatables = new IUpdatable[]
            {
                new QuitResetControllerKeyboard(agent), StateAgent.Player, this
            };
            _cameraDrawables = new IDrawable[]
            {
                StateAgent.Player
            };
        }

        public void Update()
        {
            _menuDelay.Update();
            if (_menuDelay.Delayed) return;

            _updatables = new IUpdatable[]
            {
                _screen,
                _controllerKeyboard
            };
            _fixedDrawables = new IDrawable[]
            {
                _screen
            };
            _cameraDrawables = new IDrawable[] { };
        }
    }
}
