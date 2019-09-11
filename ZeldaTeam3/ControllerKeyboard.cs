using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class ControllerKeyboard : IController
    {
        private readonly Dictionary<Keys, ICommand> _keymap;

        public ControllerKeyboard(ZeldaGame zeldaGame)
        {
            var quit = new Commands.Quit(zeldaGame);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.NumPad0, quit },
                { Keys.D0, quit },
            };
        }

        public void Update()
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if (_keymap.ContainsKey(key)) _keymap[key].Execute();
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<Keys, ICommand> keyCommand in _keymap)
            {
                result += keyCommand.Key + " - " + keyCommand.Value + "\n";
            }

            return result;
        }
    }
}
