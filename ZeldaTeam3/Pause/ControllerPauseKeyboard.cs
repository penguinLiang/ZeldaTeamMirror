using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda.Pause
{
    internal class ControllerPauseKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _pauseDirections;

        private Keys[] _lastKeys = { };

        public ControllerPauseKeyboard(GameStateAgent agent, PauseMenu pauseMenu)
        {
            var selectUp = new Commands.MenuSelectUp(pauseMenu);
            var selectDown = new Commands.MenuSelectDown(pauseMenu);
            var selectRight = new Commands.MenuSelectRight(pauseMenu);
            var selectLeft = new Commands.MenuSelectLeft(pauseMenu);

            _pauseDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.W, selectUp},
                {Keys.A, selectLeft},
                {Keys.S, selectDown},
                {Keys.D, selectRight},
                {Keys.Up, selectUp},
                {Keys.Left, selectLeft},
                {Keys.Down, selectDown},
                {Keys.Right, selectRight},

                {Keys.Space, new Commands.Resume(agent) },
                {Keys.R, new Commands.Reset(agent) },
                {Keys.Q, new Commands.Quit(agent) },
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in _lastKeys)
            {
                if (!keysPressed.Contains(key) && _pauseDirections.ContainsKey(key))
                {
                    _pauseDirections[key].Execute();
                }
            }

            _lastKeys = keysPressed;
        }
    }
}
