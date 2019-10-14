using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    internal class MouseController : IUpdatable
    {
        private readonly List<Rectangle> _roommap;
        private IScene _scene;

        public MouseController(ZeldaGame zeldaGame)
        { 
            _scene = new DummyScene();

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
                new Rectangle(425,342,85,59),
            };
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            Point mousePos = new Point(mouseState.X, mouseState.y);
            foreach (Rectangle room in _roommap)
            {
                if(mouseState.LeftButton == ButtonState.Pressed && room.Contains(mousePos))
                {
                    int row = -1;
                    int column = -1;
                    if(mouseState.X <= 85)
                    {
                        column = 0;
                    } 
                    if(mouseState.X > 85 && mouseState.X <= 170)
                    {
                        column = 1;
                    }
                    if(mouseState.X > 170 && mouseState.X <= 255)
                    {
                        column = 2;
                    }
                    if(mouseState.X > 255 && mouseState.X <= 340)
                    {
                        column = 3;
                    }
                    if(mouseState.X > 340 && mouseState.X <= 425)
                    {
                        column = 4;
                    }
                    if (mouseState.X > 425)
                    {
                        column = 5;
                    }

                    if (mouseState.Y > 47 && mouseState.Y <= 106)
                    {
                        row = 0;
                    }
                    if (mouseState.Y > 106 && mouseState.Y <= 165)
                    {
                        row = 1;
                    }
                    if (mouseState.Y > 165 && mouseState.Y <= 224)
                    {
                        row = 2;
                    }
                    if (mouseState.Y > 224 && mouseState.Y <= 283)
                    {
                        row = 3;
                    }
                    if (mouseState.Y > 283 && mouseState.Y <= 342)
                    {
                        row = 4;
                    }
                    if (mouseState.Y > 342 && mouseState.Y <= 401)
                    {
                        row = 5;
                    }
                    var transitionRoom = new Commands.SceneTransitionCommand(_scene,row,column);
                    transitionRoom.Execute();
                }
            }

        }

        public override string ToString()
        {
            var result = "";
            foreach (Rectangle keyCommand in _keymap)
            {
                result += KeyListing(keyCommand);
            }
            result += "\n";

            return result;
        }
    }
}
