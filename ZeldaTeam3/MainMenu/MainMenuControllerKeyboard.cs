using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;

namespace Zelda.MainMenu
{
    public class ScoreboardControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { };

        public ScoreboardControllerKeyboard(IMenu menu)
        {
            var selectUp = new MenuSelectUp(menu);
            var selectDown = new MenuSelectDown(menu);
            var selectOption = new MenuSelectChoice(menu);

            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Up, selectUp },
                { Keys.Down, selectDown },
                { Keys.Enter, selectOption }
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