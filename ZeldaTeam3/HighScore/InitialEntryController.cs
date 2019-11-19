using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
namespace Zelda.HighScore
{
    internal class InitialEntryController : IUpdatable
    {
        private const int LengthMax = 3;
        private static readonly Dictionary<Keys, char> KeyletterMap = new Dictionary<Keys, char>
        {
            { Keys.A, 'A' },
            { Keys.B, 'B' },
            { Keys.C, 'C' },
            { Keys.D, 'D' },
            { Keys.E, 'E' },
            { Keys.F, 'F' },
            { Keys.G, 'G' },
            { Keys.H, 'H' },
            { Keys.I, 'I' },
            { Keys.J, 'J' },
            { Keys.K, 'K' },
            { Keys.L, 'L' },
            { Keys.M, 'M' },
            { Keys.N, 'N' },
            { Keys.O, 'O' },
            { Keys.P, 'P' },
            { Keys.Q, 'Q' },
            { Keys.R, 'R' },
            { Keys.S, 'S' },
            { Keys.T, 'T' },
            { Keys.U, 'U' },
            { Keys.V, 'V' },
            { Keys.W, 'W' },
            { Keys.X, 'X' },
            { Keys.Y, 'Y' },
            { Keys.Z, 'Z' },
            { Keys.D0, '0' },
            { Keys.D1, '1' },
            { Keys.D2, '2' },
            { Keys.D3, '3' },
            { Keys.D4, '4' },
            { Keys.D5, '5' },
            { Keys.D6, '6' },
            { Keys.D7, '7' },
            { Keys.D8, '8' },
            { Keys.D9, '9' }
        };

        private Keys[] _lastKeys = {};
        private string _text = "";
        public Action<string> OnSubmit { private get; set; }
        public Action<string> OnUpdate { private get; set; }

        public void Update()
        {
            var keysPressed = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysPressed)
            {
                if (_lastKeys.Contains(key)) continue;

                if (KeyletterMap.ContainsKey(key) && _text.Length < LengthMax )
                {
                    _text += KeyletterMap[key];
                    OnUpdate?.Invoke(_text);
                }
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (key)
                {
                    case Keys.Back when _text.Length > 0:
                        _text = _text.Remove(_text.Length - 1);
                        OnUpdate?.Invoke(_text);
                        break;
                    case Keys.Enter:
                        OnUpdate?.Invoke(_text);
                        OnSubmit?.Invoke(_text);
                        break;
                }
            }

            _lastKeys = keysPressed;
        }
    }
}
