using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class ControllerKeyboard : IController
    {
        private readonly Dictionary<Keys, ICommand> _keymap;

        public ControllerKeyboard(ZeldaGame zeldaGame, IPlayer Link)
        { 
            var quit = new Commands.Quit(zeldaGame);
            var attack = new Commands.LinkPrimaryAction(Link);
            var secondary = new Commands.LinkSecondaryAction(Link);
            var damage = new Commands.LinkDamage(Link);
            var up = new Commands.LinkUp(Link);
            var down = new Commands.LinkDown(Link);
            var right = new Commands.LinkRight(Link);
            var left = new Commands.LinkLeft(Link);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.NumPad0, quit },
                { Keys.D0, quit },
                { Keys.N, attack },
                { Keys.Z, attack },
                { Keys.X, secondary },
                { Keys.M, secondary },
                { Keys.Up, up },
                { Keys.W, up },
                { Keys.Left, left },
                { Keys.A, left },
                { Keys.D, right },
                { Keys.Right, right },
                { Keys.S, down },
                { Keys.Down, down },
                { Keys.E, damage }
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
