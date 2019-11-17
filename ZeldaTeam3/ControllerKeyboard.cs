using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;
using Zelda.GameState;
using Zelda.Items;

// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
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
            var quit = new Quit(agent);

            var primaryattack = new LinkPrimaryAction(agent.Player);
            var whiteswordupgrade = new UpgradeSword(agent.Player, Primary.WhiteSword);
            var magicalswordupgrade = new UpgradeSword(agent.Player, Primary.MagicalSword);

            var secondaryattack = new LinkSecondaryAction(agent.Player);
            var bowassign = new LinkBowAssign(agent.Player);
            var firebowassign = new LinkSecondaryAssign(agent.Player, Secondary.FireBow);
            var boomerangassign = new LinkBoomerangAssign(agent.Player);
            var bombassign = new LinkBombAssign(agent.Player);
            var coinassign = new LinkSecondaryAssign(agent.Player, Secondary.Coins);
            var atwboomerangassign = new LinkSecondaryAssign(agent.Player, Secondary.ATWBoomerang);
            var bomblauncherassign = new LinkSecondaryAssign(agent.Player, Secondary.BombLauncher);

            var up = new LinkMoveUp(agent.Player);
            var down = new LinkMoveDown(agent.Player);
            var right = new LinkMoveRight(agent.Player);
            var left = new LinkMoveLeft(agent.Player);

            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quit }
            };

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Z, primaryattack },
                { Keys.D2, whiteswordupgrade },
                { Keys.D3, magicalswordupgrade },
                
                { Keys.X, secondaryattack },
                { Keys.D4, bowassign },
                { Keys.D0, firebowassign },
                { Keys.D5, boomerangassign },
                { Keys.D6, bombassign },
                { Keys.D7, coinassign },
                { Keys.D8, atwboomerangassign },
                { Keys.D9, bomblauncherassign },
                //{ Keys.D1, arrowupgrade },

                { Keys.Space, new Commands.Pause(agent) },
                { Keys.M, new ShowJumpMap(agent) },
                { Keys.R, new Reset(agent) }
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
                { Keys.Down, down }
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
