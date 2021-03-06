﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;
using Zelda.Survival.GameState;
using Quit = Zelda.Survival.Commands.Quit;
using Reset = Zelda.Survival.Commands.Reset;
using Resume = Zelda.Survival.Commands.Resume;

namespace Zelda.Survival.Pause
{
    internal class ControllerPauseKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _pauseDirections;

        private Keys[] _lastKeys = { };

        public ControllerPauseKeyboard(GameStateAgent agent, IMenu pauseMenu)
        {
            var selectUp = new MenuSelectUp(pauseMenu);
            var selectDown = new MenuSelectDown(pauseMenu);
            var selectRight = new MenuSelectRight(pauseMenu);
            var selectLeft = new MenuSelectLeft(pauseMenu);

            _pauseDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.W, selectUp},
                {Keys.A, selectLeft},
                {Keys.S, selectDown},
                {Keys.D, selectRight},
                {Keys.Up, selectUp},
                {Keys.Left, selectLeft},
                {Keys.Down, selectDown},
                {Keys.Right, selectRight},

                {Keys.Space, new Resume(agent) },
                {Keys.R, new Reset(agent) },
                {Keys.Q, new Quit(agent) }
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in _lastKeys)
            {
                if (!keysPressed.Contains(key) && _pauseDirections.ContainsKey(key))
                {
                    _pauseDirections[key].Execute();
                }
            }

            _lastKeys = keysPressed;
        }
    }
}
