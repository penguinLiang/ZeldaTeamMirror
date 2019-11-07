using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda.GameOver
{
    public class GameOverControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keyDownMap;
        private readonly Dictionary<Keys, ICommand> _keyUpMap;
        private readonly Dictionary<Keys, ICommand> _keyResetMap;
        private readonly Dictionary<Keys, ICommand> _keyQuitMap;
        private readonly Dictionary<Keys, ICommand> _keySelectMap;
        private Keys[] _lastKeys = {};

        public GameOverControllerKeyboard(GameStateAgent agent, GameOverMenu gameOverMenu)
        {
            var selectUp = new Commands.MenuSelectUp(gameOverMenu);
            var selectDown = new Commands.MenuSelectDown(gameOverMenu);

            _keyQuitMap = new Dictionary<Keys, ICommand> { { Keys.Q, new Commands.Quit(agent) } };
            _keyResetMap = new Dictionary<Keys, ICommand> { { Keys.R, new Commands.Reset(agent) } };
            _keySelectMap = new Dictionary<Keys, ICommand>
            {
                {Keys.Enter, new Commands.MenuSelectChoice(gameOverMenu) },
            };

            _keyDownMap = new Dictionary<Keys, ICommand>
            {
          
                {Keys.S, selectDown},
                {Keys.Down, selectDown}
            };

            _keyUpMap = new Dictionary<Keys, ICommand>
            {
                {Keys.W, selectUp},
                {Keys.Up, selectUp},
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keyDownMap.ContainsKey(key)) _keyDownMap[key].Execute();
                if (_keyQuitMap.ContainsKey(key)) _keyQuitMap[key].Execute();
                if (_keyResetMap.ContainsKey(key)) _keyResetMap[key].Execute();
                if (_keyUpMap.ContainsKey(key)) _keyUpMap[key].Execute();
                if (_keySelectMap.ContainsKey(key)) _keySelectMap[key].Execute();
            }

            foreach (var key in _lastKeys)
            {
                if (!keysPressed.Contains(key) && _keyUpMap.ContainsKey(key))
                {
                    _keyUpMap[key].Execute();
                }
                if (!keysPressed.Contains(key) && _keyDownMap.ContainsKey(key))
                {
                    _keyDownMap[key].Execute();
                }
                if (!keysPressed.Contains(key) && _keyQuitMap.ContainsKey(key))
                {
                    _keyQuitMap[key].Execute();
                }
                if (!keysPressed.Contains(key) && _keyResetMap.ContainsKey(key))
                {
                    _keyResetMap[key].Execute();
                }
                if (!keysPressed.Contains(key) && _keySelectMap.ContainsKey(key))
                {
                    _keySelectMap[key].Execute();
                }
            }

            _lastKeys = keysPressed;
        }
    }
}
