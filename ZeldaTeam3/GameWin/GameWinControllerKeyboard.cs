using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.GameState;

namespace Zelda.GameWin
{
   internal class GameWinControllerKeyboard : IUpdatable
    {

            private readonly Dictionary<Keys, ICommand> _winDirections;

            private Keys[] _lastKeys = { };

            public GameWinControllerKeyboard(GameStateAgent agent, GameWinMenu winMenu)
            {
                var selectUp = new Commands.MenuSelectUp(winMenu);
                var selectDown = new Commands.MenuSelectDown(winMenu);

                _winDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.W, selectUp},            
                {Keys.S, selectDown},
                {Keys.Up, selectUp},
                {Keys.Down, selectDown},

                {Keys.R, new Commands.Reset(agent) },
                {Keys.Q, new Commands.Quit(agent) },
            };
            }

            public void Update()
            {
                var keysPressed = Keyboard.GetState().GetPressedKeys();

                foreach (var key in _lastKeys)
                {
                    if (!keysPressed.Contains(key) && _winDirections.ContainsKey(key))
                    {
                        _winDirections[key].Execute();
                    }
                }

                _lastKeys = keysPressed;
            }
        }

    }

