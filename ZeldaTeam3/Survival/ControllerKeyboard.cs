using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;
using Zelda.Survival.GameState;
using Zelda.Items;

// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
namespace Zelda.Survival
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
            var up = new LinkMoveUp(agent.Player);
            var down = new LinkMoveDown(agent.Player);
            var right = new LinkMoveRight(agent.Player);
            var left = new LinkMoveLeft(agent.Player);

            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Commands.Quit(agent) }
            };

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Z, new LinkPrimaryAction(agent.Player)},
                { Keys.D2, new UpgradeSword(agent.Player, Primary.WhiteSword)},
                { Keys.D3, new UpgradeSword(agent.Player, Primary.MagicalSword)},

                { Keys.X, new LinkSecondaryAction(agent.Player)},
                { Keys.D4, new LinkBowAssign(agent.Player)},
                { Keys.D5, new LinkBoomerangAssign(agent.Player)},
                { Keys.D6, new LinkBombAssign(agent.Player)},
                { Keys.D7, new LinkSecondaryAssign(agent.Player, Secondary.Coins)},
                { Keys.D8, new LinkSecondaryAssign(agent.Player, Secondary.ATWBoomerang)},
                { Keys.D9, new LinkSecondaryAssign(agent.Player, Secondary.BombLauncher)},
                { Keys.D0, new LinkSecondaryAssign(agent.Player, Secondary.FireBow)},

                { Keys.Space, new Commands.Pause(agent) },
                { Keys.R, new Commands.Reset(agent) }
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
    }
}
