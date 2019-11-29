using System;
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
        private readonly GameStateAgent _agent;
        private readonly Dictionary<Keys, ICommand> _keydownMap;
        private readonly Dictionary<Keys, ICommand> _keyupMap;
        private readonly Dictionary<Keys, ICommand> _playerDirections;

        private Keys _firstPlayerDirection = Keys.None;
        private Keys[] _lastKeys = {};

        private readonly Keys[] _konamiCode =
        {
            Keys.Up,
            Keys.Up,
            Keys.Down,
            Keys.Down,
            Keys.Left,
            Keys.Right,
            Keys.Left,
            Keys.Right,
            Keys.A,
            Keys.B,
            Keys.Enter
        };
        private int _konamiPos;

        public ControllerKeyboard(GameStateAgent agent)
        {
            _agent = agent;

            var up = new LinkMoveUp(agent.Player);
            var down = new LinkMoveDown(agent.Player);
            var right = new LinkMoveRight(agent.Player);
            var left = new LinkMoveLeft(agent.Player);

            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Quit(agent) }
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
                if (keysPressed.Contains(key)) continue;
                if (_keyupMap.ContainsKey(key))
                {
                    _keyupMap[key].Execute();
                }

                if (_konamiPos == _konamiCode.Length) continue;

                if (_konamiCode[_konamiPos] == key)
                {
                    if (++_konamiPos == _konamiCode.Length)
                    {
                        Console.WriteLine("PARTY HARD");
                        _agent.PartyHard();
                    }
                }
                else
                {
                    _konamiPos = 0;
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
