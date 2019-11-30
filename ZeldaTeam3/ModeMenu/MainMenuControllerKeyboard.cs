using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;

namespace Zelda.ModeMenu
{
    public class MainMenuControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keyupMap;
        private Keys[] _lastKeys = { };

        public MainMenuControllerKeyboard(IMenu menu)
        {
            var selectUp = new MenuSelectUp(menu);
            var selectDown = new MenuSelectDown(menu);
            var selectChoice = new MenuSelectChoice(menu);

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Up, selectUp },
                { Keys.Down, selectDown },
                { Keys.Enter, selectChoice }
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

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
