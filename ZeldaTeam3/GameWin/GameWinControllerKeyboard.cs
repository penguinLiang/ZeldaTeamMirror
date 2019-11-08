using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Zelda.GameState;

namespace Zelda.GameWin
{
   internal class GameWinControllerKeyboard : IUpdatable
    {
            private readonly Dictionary<Keys, ICommand> _keydownMap;
            private readonly Dictionary<Keys, ICommand> _keyupMap;

            private Keys[] _lastKeys = { };

            public GameWinControllerKeyboard(GameStateAgent agent, GameWinMenu winMenu)
            {
            var selectUp = new Commands.MenuSelectUp(winMenu);
            var selectDown = new Commands.MenuSelectDown(winMenu);
            var selectChoice = new Commands.MenuSelectChoice(winMenu);
                
                _keydownMap = new Dictionary<Keys, ICommand>
            {
                {Keys.R, new Commands.Reset(agent) },
                {Keys.Q, new Commands.Quit(agent) },
                {Keys.Enter, selectChoice },
                {Keys.Down, selectDown },
                {Keys.S, selectDown },
                {Keys.Up, selectUp },
                {Keys.W, selectUp }
            };

            _keyupMap = new Dictionary<Keys, ICommand> {
                {Keys.R, new Commands.Reset(agent) },
            };

        }

            public void Update()
            {
                var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownMap.ContainsKey(key) && !_lastKeys.Contains(key))
                {
                    _keydownMap[key].Execute();
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

