using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda
{
    internal class ControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownMap;
        private readonly Dictionary<Keys, ICommand> _keyupMap;
        private readonly Dictionary<Keys, ICommand> _playerDirections;

        private Keys _firstPlayerDirection = Keys.None;
        private Keys[] _lastKeys = {};

        public ControllerKeyboard(GameStateAgent agent)
        { 
            var quit = new Commands.Quit(agent);

            var primaryattack = new Commands.LinkPrimaryAction(agent.Player);
            var whiteswordupgrade = new Commands.UpgradeSword(agent.Player, Items.Primary.WhiteSword);
            var magicalswordupgrade = new Commands.UpgradeSword(agent.Player, Items.Primary.MagicalSword);

            var secondaryattack = new Commands.LinkSecondaryAction(agent.Player);
            var bowassign = new Commands.LinkBowAssign(agent.Player);
            var boomerangassign = new Commands.LinkBoomerangAssign(agent.Player);
            var bombassign = new Commands.LinkBombAssign(agent.Player);

            var up = new Commands.LinkMoveUp(agent.Player);
            var down = new Commands.LinkMoveDown(agent.Player);
            var right = new Commands.LinkMoveRight(agent.Player);
            var left = new Commands.LinkMoveLeft(agent.Player);

            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quit },
            };

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Z, primaryattack },
                { Keys.D2, whiteswordupgrade },
                { Keys.D3, magicalswordupgrade },
                
                { Keys.X, secondaryattack },
                { Keys.D4, bowassign },
                { Keys.D5, boomerangassign },
                { Keys.D6, bombassign },

                { Keys.Space, new Commands.Pause(agent) },
                { Keys.M, new Commands.ShowJumpMap(agent) },
                { Keys.R, new Commands.Reset(agent) },
               
            };

            _playerDirections = new Dictionary<Keys, ICommand>
            {
                { Keys.W, up },
                { Keys.A, left },
                { Keys.S, down },
                { Keys.D, right },
                { Keys.Up, up },
                { Keys.Left, left },
                { Keys.Right, right },
                { Keys.Down, down },
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownMap.ContainsKey(key)) _keydownMap[key].Execute();

                if (_playerDirections.ContainsKey(key) && !keysPressed.Contains(_firstPlayerDirection))
                {
                    _firstPlayerDirection = key;
                }

                if (_firstPlayerDirection == key)
                {
                    _playerDirections[key].Execute();
                }

            }

            foreach (var key in _lastKeys)
            {
                if (!keysPressed.Contains(key) && _keyupMap.ContainsKey(key))
                {
                    _keyupMap[key].Execute();
                }
            }

            _lastKeys = keysPressed;
        }

        private static string KeyListing(KeyValuePair<Keys, ICommand> keyCommand) => keyCommand.Key + " - " + keyCommand.Value + "\n";

        public override string ToString()
        {
            var result = "";
            foreach (var keyCommand in _keydownMap)
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
