using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    internal class ControllerPauseKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _pauseDirections;

        private Keys[] _lastKeys = { };
        private Keys _firstPlayerDirection = Keys.None;

        public ControllerPauseKeyboard(ZeldaGame zeldaGame)
        { 
            var selectUp = new Commands.MenuSelectUp(zeldaGame.PauseMenu);
            var selectDown = new Commands.MenuSelectDown(zeldaGame.PauseMenu);
            var selectRight = new Commands.MenuSelectRight(zeldaGame.PauseMenu);
            var selectLeft = new Commands.MenuSelectLeft(zeldaGame.PauseMenu);
            var exit = new Commands.MenuUnpause(zeldaGame.PauseMenu);

            _pauseDirections = new Dictionary<Keys, ICommand>
            {
                {Keys.W, selectUp},
                {Keys.A, selectLeft},
                {Keys.S, selectDown},
                {Keys.D, selectRight},

                {Keys.Space, exit },

                {Keys.Up, selectUp},
                {Keys.Left, selectLeft},
                {Keys.Down, selectDown},
                {Keys.Right, selectRight}
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

        private static string KeyListing(KeyValuePair<Keys, ICommand> keyCommand) => keyCommand.Key + " - " + keyCommand.Value + "\n";

        public override string ToString()
        {
            var result = "";

            foreach (var keyCommand in _pauseDirections)
            {
                result += KeyListing(keyCommand);
            }
            result += "\n";

            return result;
        }
    }
}
