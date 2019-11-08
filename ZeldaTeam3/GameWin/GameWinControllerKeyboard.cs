using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Zelda.GameState;

namespace Zelda.GameWin
{
    internal class GameWinControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { };

        public GameWinControllerKeyboard(GameStateAgent agent, GameWinMenu winMenu)
        {
            var selectUp = new Commands.MenuSelectUp(winMenu);
            var selectDown = new Commands.MenuSelectDown(winMenu);
            var selectChoice = new Commands.MenuSelectChoice(winMenu);

            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                {Keys.R, new Commands.Reset(agent) },
                {Keys.Q, new Commands.Quit(agent) },
                {Keys.Enter, selectChoice },
                {Keys.Down, selectDown },
                {Keys.S, selectDown },
                {Keys.Up, selectUp },
                {Keys.W, selectUp }
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownOnceMap.ContainsKey(key) && !_lastKeys.Contains(key))
                    _keydownOnceMap[key].Execute();
            }

            _lastKeys = keysPressed;
        }
    }

}

