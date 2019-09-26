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
            var reset = new Commands.Reset(zeldaGame);

            var attack = new Commands.LinkPrimaryAction(zeldaGame.Link);

            var swordassign = new Commands.LinkSwordAssign(zeldaGame.Link);
            var whiteswordassign = new Commands.LinkWhiteSwordAssign(zeldaGame.Link);
            var magicswordassign = new Commands.LinkMagicSwordAssign(zeldaGame.Link);
            var bowassign = new Commands.LinkBowAssign(zeldaGame.Link);
            var boomerangassign = new Commands.LinkBoomerangAssign(zeldaGame.Link);
            var bombassign = new Commands.LinkBombAssign(zeldaGame.Link);

            var damage = new Commands.LinkDamage(zeldaGame.Link);
            var up = new Commands.LinkUp(zeldaGame.Link);
            var down = new Commands.LinkDown(zeldaGame.Link);
            var right = new Commands.LinkRight(zeldaGame.Link);
            var left = new Commands.LinkLeft(zeldaGame.Link);

            var enemyup = new Commands.EnemyUp(zeldaGame.Enemies);
            var enemydown = new Commands.EnemyDown(zeldaGame.Enemies);
            var enemyleft = new Commands.EnemyLeft(zeldaGame.Enemies);
            var enemyright = new Commands.EnemyRight(zeldaGame.Enemies);

            var enemyspawn = new Commands.EnemySpawn(zeldaGame.Enemies);
            var enemydamage = new Commands.EnemyDamage(zeldaGame.Enemies);
            var enemykill = new Commands.EnemyKill(zeldaGame.Enemies);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quit },
                { Keys.R, reset },

                { Keys.N, attack },
                { Keys.Z, attack },

                { Keys.D1, swordassign },
                { Keys.D2, whiteswordassign },
                { Keys.D3, magicswordassign },
                { Keys.D4, bowassign },
                { Keys.D5, boomerangassign },
                { Keys.D6, bombassign },

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

                { Keys.T, enemyspawn },
                { Keys.Y, enemydamage},
                { Keys.I, enemykill },
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
