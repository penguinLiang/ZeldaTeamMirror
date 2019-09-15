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

            var swordassign = new Commands.LinkSwordAssign(zeldaGame.TemporaryLink);
            var whiteswordassign = new Commands.LinkWhiteSwordAssign(zeldaGame.TemporaryLink);
            var magicswordassign = new Commands.LinkMagicSwordAssign(zeldaGame.TemporaryLink);
            var bowassign = new Commands.LinkBowAssign(zeldaGame.TemporaryLink);
            var boomerangassign = new Commands.LinkBoomerangAssign(zeldaGame.TemporaryLink);
            var bombassign = new Commands.LinkBombAssign(zeldaGame.TemporaryLink);

            var damage = new Commands.LinkDamage(zeldaGame.TemporaryLink);
            var up = new Commands.LinkUp(zeldaGame.TemporaryLink);
            var down = new Commands.LinkDown(zeldaGame.TemporaryLink);
            var right = new Commands.LinkRight(zeldaGame.TemporaryLink);
            var left = new Commands.LinkLeft(zeldaGame.TemporaryLink);

            var enemyup = new Commands.EnemyUp(zeldaGame.Enemies);
            var enemydown = new Commands.EnemyDown(zeldaGame.Enemies);
            var enemyleft = new Commands.EnemyLeft(zeldaGame.Enemies);
            var enemyright = new Commands.EnemyRight(zeldaGame.Enemies);

            var enemysequence = new Commands.EnemyDamagePauseKill(zeldaGame.Enemies);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.NumPad0, quit },
                { Keys.D0, quit },

                { Keys.N, attack },
                { Keys.Z, attack },

                { Keys.NumPad1, swordassign },
                { Keys.NumPad2, whiteswordassign },
                { Keys.NumPad3, magicswordassign },
                { Keys.NumPad4, bowassign },
                { Keys.NumPad5, boomerangassign },
                { Keys.NumPad6, bombassign },

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
                { Keys.K, enemyright },

                { Keys.T, enemysequence },
                { Keys.Y, enemysequence },
                { Keys.I, enemysequence },
                { Keys.O, enemysequence },
                { Keys.P, enemysequence },
                { Keys.G, enemysequence },
                { Keys.L, enemysequence },
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
