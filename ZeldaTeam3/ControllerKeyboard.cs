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

            var attack = new Commands.LinkPrimaryAction(zeldaGame.TemporaryLink);
            var secondary = new Commands.LinkSecondaryAction(zeldaGame.TemporaryLink);
            var damage = new Commands.LinkDamage(zeldaGame.TemporaryLink);
            var up = new Commands.LinkUp(zeldaGame.TemporaryLink);
            var down = new Commands.LinkDown(zeldaGame.TemporaryLink);
            var right = new Commands.LinkRight(zeldaGame.TemporaryLink);
            var left = new Commands.LinkLeft(zeldaGame.TemporaryLink);

            var enemyup = new Commands.EnemyUp(zeldaGame.TemporaryEnemy);
            var enemydown = new Commands.EnemyDown(zeldaGame.TemporaryEnemy);
            var enemyleft = new Commands.EnemyLeft(zeldaGame.TemporaryEnemy);
            var enemyright = new Commands.EnemyRight(zeldaGame.TemporaryEnemy);

            var enemysequence = new Commands.EnemyDamagePauseKill(zeldaGame.TemporaryEnemy);

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
                { Keys.E, damage },

                { Keys.U, enemyup },
                { Keys.H, enemyleft },
                { Keys.J, enemydown },
                { Keys.K, enemyright }
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
