using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class ControllerKeyboard : IController
    {
        private readonly Dictionary<Keys, ICommand> _keymap;

        public ControllerKeyboard(ZeldaGame zeldaGame)
        {
            var quit = new CommandQuit(zeldaGame);
            var setFixedStatic = new CommandSetFixedStatic(zeldaGame);
            var setFixedAnimated = new CommandSetFixedAnimated(zeldaGame);
            var setMovingStatic = new CommandSetMovingStatic(zeldaGame);
            var setMovingAnimated = new CommandSetMovingAnimated(zeldaGame);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.NumPad0, quit },
                { Keys.D0, quit },
                { Keys.NumPad1, setFixedStatic },
                { Keys.D1, setFixedStatic },
                { Keys.NumPad2, setFixedAnimated },
                { Keys.D2, setFixedAnimated },
                { Keys.NumPad3, setMovingStatic },
                { Keys.D3, setMovingStatic },
                { Keys.NumPad4, setMovingAnimated },
                { Keys.D4, setMovingAnimated }
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
    }
}
