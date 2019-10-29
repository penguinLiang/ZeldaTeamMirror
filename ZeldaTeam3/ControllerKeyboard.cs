using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    internal class ControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keymap;
        private readonly Dictionary<Keys, ICommand> _playerDirections;

        private Keys _firstPlayerDirection = Keys.None;

        public ControllerKeyboard(ZeldaGame zeldaGame)
        { 
            var quit = new Commands.Quit(zeldaGame);
            var reset = new Commands.Reset(zeldaGame);

            var attack = new Commands.LinkPrimaryAction(zeldaGame.Link);

            var bowassign = new Commands.LinkBowAssign(zeldaGame.Link);
            var boomerangassign = new Commands.LinkBoomerangAssign(zeldaGame.Link);
            var bombassign = new Commands.LinkBombAssign(zeldaGame.Link);

            var damage = new Commands.SpawnableDamage(zeldaGame.Link);
            var up = new Commands.LinkMoveUp(zeldaGame.Link);
            var down = new Commands.LinkMoveDown(zeldaGame.Link);
            var right = new Commands.LinkMoveRight(zeldaGame.Link);
            var left = new Commands.LinkMoveLeft(zeldaGame.Link);

            _keymap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quit },
                { Keys.R, reset },

                { Keys.N, attack },
                { Keys.Z, attack },
                { Keys.E, damage},

                { Keys.D4, bowassign },
                { Keys.D5, boomerangassign },
                { Keys.D6, bombassign },

                { Keys.NumPad4, bowassign },
                { Keys.NumPad5, boomerangassign },
                { Keys.NumPad6, bombassign },

                {Keys.K, new Commands.LinkKnockback(zeldaGame.Link) },

                {Keys.M, new Commands.ShowJumpMap(zeldaGame) }
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
                {Keys.Down, down}
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keymap.ContainsKey(key)) _keymap[key].Execute();

                if (_playerDirections.ContainsKey(key) && !keysPressed.Contains(_firstPlayerDirection))
                {
                    _firstPlayerDirection = key;
                }

                if (_firstPlayerDirection == key)
                {
                    _playerDirections[key].Execute();
                }
            }

        }

        private static string KeyListing(KeyValuePair<Keys, ICommand> keyCommand) => keyCommand.Key + " - " + keyCommand.Value + "\n";

        public override string ToString()
        {
            var result = "";
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

            return result;
        }
    }
}
