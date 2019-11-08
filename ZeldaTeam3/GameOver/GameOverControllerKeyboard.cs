using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda.GameOver
{
    public class GameOverControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { };

        public GameOverControllerKeyboard(GameStateAgent agent, GameOverMenu gameOverMenu)
        {
            var selectUp = new Commands.MenuSelectUp(gameOverMenu);
            var selectDown = new Commands.MenuSelectDown(gameOverMenu);
            var selectChoice = new Commands.MenuSelectChoice(gameOverMenu);

            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Commands.Quit(agent) },
                { Keys.R, new Commands.Reset(agent) },

                { Keys.S, selectDown },
                { Keys.Down, selectDown },
                { Keys.W, selectUp },
                { Keys.Up, selectUp },
                { Keys.Enter, selectChoice },
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