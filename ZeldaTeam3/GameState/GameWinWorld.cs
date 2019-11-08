using Microsoft.Xna.Framework;
using Zelda.GameOver;
using Zelda.GameWin;
using Zelda.HUD;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class GameWinWorld : GameWorld, IUpdatable
    {

        private IUpdatable[] _updatables;
        public override IUpdatable[] Updatables => _updatables;

        private readonly GameWinMenu _screen;
        private readonly FrameDelay _menuDelay = new FrameDelay(300);
        private readonly GameWinControllerKeyboard _controllerKeyboard;


        private IDrawable[] _scaledDrawables;
        public override IDrawable[] ScaledDrawables => _scaledDrawables;
        public GameWinWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayWinMusic();

            _screen = new GameWinMenu(agent);
            _controllerKeyboard = new GameWinControllerKeyboard(agent, _screen);
            _updatables = new IUpdatable[]
            {
                new GameWinControllerKeyboard(agent, _screen),
                StateAgent.Player,
                this
            };

            _scaledDrawables = new IDrawable[]
            {
                //StateAgent.Player,
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
