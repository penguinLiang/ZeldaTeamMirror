using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Commands;
using Zelda.GameState;

namespace Zelda.GameWin
{
    internal class GameWinControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { };

        public GameWinControllerKeyboard(GameStateAgent agent, IMenu winMenu)
        {
            var selectUp = new MenuSelectUp(winMenu);
            var selectDown = new MenuSelectDown(winMenu);
            var selectChoice = new MenuSelectChoice(winMenu);

            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                {Keys.R, new Reset(agent) },
                {Keys.Q, new Quit(agent) },
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

