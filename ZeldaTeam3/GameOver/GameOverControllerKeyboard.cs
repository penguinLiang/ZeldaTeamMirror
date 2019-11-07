
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda.GameOver
{
    public class GameOverControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownMap;
        private readonly Dictionary<Keys, ICommand> _keyupMap;
        private Keys[] _lastKeys = { };

        public GameOverControllerKeyboard(GameStateAgent agent, GameOverMenu gameOverMenu)
        {
            var selectUp = new Commands.MenuSelectUp(gameOverMenu);
            var selectDown = new Commands.MenuSelectDown(gameOverMenu);
            var selectChoice = new Commands.MenuSelectChoice(gameOverMenu);

            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Commands.Quit(agent) },
                {Keys.S, selectDown},
                {Keys.Down, selectDown},
                {Keys.W, selectUp},
                {Keys.Up, selectUp},
                {Keys.Enter, selectChoice }
            };

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.M, new Commands.Play(agent) },
                { Keys.R, new Commands.Reset(agent) },
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownMap.ContainsKey(key) && !_lastKeys.Contains(key)) _keydownMap[key].Execute();
            }

            foreach (var key in _lastKeys)
            {
                if (!keysPressed.Contains(key) && _keyupMap.ContainsKey(key))
                {
                    _keyupMap[key].Execute();
                }
            }

            _lastKeys = keysPressed;
        }
    }
}