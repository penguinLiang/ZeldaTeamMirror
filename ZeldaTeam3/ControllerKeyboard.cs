using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class ControllerKeyboard : IController
    {
        private readonly Dictionary<Keys, ICommand> _keymap;
        private readonly Dictionary<Keys, ICommand> _playerDirections;
        private readonly Dictionary<Keys, ICommand> _enemyDirections;

        private Keys _firstPlayerDirection = Keys.None;
        private Keys _firstEnemyDirection = Keys.None;

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
                { Keys.E, damage},

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

                { Keys.T, enemyspawn },
                { Keys.Y, enemydamage},
                { Keys.I, enemykill }
            };

            _playerDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.W, up},
                {Keys.A, left},
                {Keys.S, down},
                {Keys.D, right},
                {Keys.Up, up},
                {Keys.Left, left},
                {Keys.Right, right},
                {Keys.Down, down},
            };

            _enemyDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.U, enemyup},
                {Keys.H, enemyleft},
                {Keys.J, enemydown},
                {Keys.K, enemyright}
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if (_keymap.ContainsKey(key)) _keymap[key].Execute();

                if (_playerDirections.ContainsKey(key) && !keysPressed.Contains(_firstPlayerDirection))
                {
                    _firstPlayerDirection = key;
                }

                if (_enemyDirections.ContainsKey(key) && !keysPressed.Contains(_firstEnemyDirection))
                {
                    _firstEnemyDirection = key;
                }

                if (_firstPlayerDirection == key)
                {
                    _playerDirections[key].Execute();
                }

                if (_firstEnemyDirection == key)
                {
                    _enemyDirections[key].Execute();
                }
            }

        }

        private string KeyListing(KeyValuePair<Keys, ICommand> keyCommand) => keyCommand.Key + " - " + keyCommand.Value + "\n";

        public override string ToString()
        {
            string result = "";
            foreach (var keyCommand in _keymap)
            {
                result += KeyListing(keyCommand);
            }
            result += "\n";

            foreach (var keyCommand in _playerDirections)
            {
                result += KeyListing(keyCommand);
            }
            result += "\n";

            foreach (var keyCommand in _enemyDirections)
            {
                result += KeyListing(keyCommand);
            }

            return result;
        }
    }
}
