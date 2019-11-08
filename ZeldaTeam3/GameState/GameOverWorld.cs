using Zelda.GameOver;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class GameOverWorld : GameWorld, IUpdatable
    {
        private IUpdatable[] _updatables;
        private IDrawable[] _scaledDrawables;
        public override IUpdatable[] Updatables => _updatables;
        public override IDrawable[] ScaledDrawables => _scaledDrawables;

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
            _scaledDrawables = new IDrawable[]
            {
                StateAgent.Player,
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
            _scaledDrawables = new IDrawable[]
            {
                _screen
            };
        }
    }
}
