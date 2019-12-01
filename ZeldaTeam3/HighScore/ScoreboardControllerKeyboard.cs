using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.Survival.Commands;
using Zelda.Survival.GameState;

namespace Zelda.HighScore
{
    public class ScoreboardControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownOnceMap;
        private Keys[] _lastKeys = { Keys.Enter, Keys.Q, Keys.R };

        public ScoreboardControllerKeyboard(GameStateAgent agent)
        {
            var reset = new Reset(agent);
            _keydownOnceMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Quit(agent) },
                { Keys.R, reset},

                { Keys.Enter, reset }
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