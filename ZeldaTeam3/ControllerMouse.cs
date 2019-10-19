using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Zelda
{
    internal class ControllerMouse : IUpdatable
    {
        private readonly List<Rectangle> _roommap;
        private readonly ZeldaGame _zeldaGame;

        public ControllerMouse(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;

            _roommap = new List<Rectangle>
            {
                //Row zero rooms
                new Rectangle(85,47,85,59),
                new Rectangle(170,47,85,59),

                //Row one rooms
                new Rectangle(85,106,85,59),
                new Rectangle(170,106,85,59),
                new Rectangle(340,106,85,59),
                new Rectangle(425,106,85,59),

                //Row two rooms
                new Rectangle(0,165,85,59),
                new Rectangle(85,165,85,59),
                new Rectangle(170,165,85,59),
                new Rectangle(255,165,85,59),
                new Rectangle(340,165,85,59),

                //Row three rooms
                new Rectangle(85,224,85,59),
                new Rectangle(170,224,85,59),
                new Rectangle(255,224,85,59),
                new Rectangle(425,224,85,59),

                //Row four rooms
                new Rectangle(0,283,85,59),
                new Rectangle(170,283,85,59),
                new Rectangle(425,283,85,59),

                //Row five rooms
                new Rectangle(85,342,85,59),
                new Rectangle(170,342,85,59),
                new Rectangle(255,342,85,59),
                new Rectangle(425,342,85,59)
            };
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            if (!_zeldaGame.JumpMap.Visible || mouseState.LeftButton != ButtonState.Pressed) return;

            var mousePos = mouseState.Position;

            if (mousePos.Y < 47 || mousePos.Y > 47 + 59*6)
            {
                _zeldaGame.JumpMap.Visible = false;
                return;
            }

            foreach (var room in _roommap)
            {
                if (!room.Contains(mousePos)) continue;

                var row = (mouseState.Y - 47) / 59;
                var column = mouseState.X / 85;

                _zeldaGame.JumpMap.Visible = false;
                _zeldaGame.DungeonManager.TransitionToRoom(row, column);
            }

        }
    }
}
