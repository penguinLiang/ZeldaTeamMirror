using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Zelda.GameState;

namespace Zelda.JumpMap
{
    public class JumpMapControllerKeyboard : IUpdatable
    {
        private readonly Dictionary<Keys, ICommand> _keydownMap;
        private readonly Dictionary<Keys, ICommand> _keyupMap;
        private Keys[] _lastKeys = {};

        public JumpMapControllerKeyboard(GameStateAgent agent)
        { 
            _keydownMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, new Commands.Quit(agent) },
            };

            _keyupMap = new Dictionary<Keys, ICommand>
            {
                { Keys.M, new Commands.Play(agent) },
                { Keys.R, new Commands.Reset(agent) },
            };
        }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_keydownMap.ContainsKey(key)) _keydownMap[key].Execute();
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
