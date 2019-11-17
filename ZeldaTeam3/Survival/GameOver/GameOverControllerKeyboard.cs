using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;
using Zelda.Survival.GameState;

namespace Zelda.Survival.GameOver
{
    public class GameOverControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { };

        public GameOverControllerKeyboard(GameStateAgent agent, IMenu gameOverMenu)
        {
            var selectUp = new MenuSelectUp(gameOverMenu);
            var selectDown = new MenuSelectDown(gameOverMenu);
            var selectChoice = new MenuSelectChoice(gameOverMenu);

            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Commands.Quit(agent) },
                { Keys.R, new Commands.Reset(agent) },

                { Keys.S, selectDown },
                { Keys.Down, selectDown },
                { Keys.W, selectUp },
                { Keys.Up, selectUp },
                { Keys.Enter, selectChoice }
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownOnceMap.ContainsKey(key) && !_lastKeys.Contains(key))
                    _keydownOnceMap[key].Execute();
            }

            _lastKeys = keysPressed;
        }
    }
}