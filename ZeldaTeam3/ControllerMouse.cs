using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    class ControllerMouse : IController
    {
        private readonly ZeldaGame _zeldaGame;
        private readonly ICommand _quit;
        private readonly ICommand _setFixedStatic;
        private readonly ICommand _setFixedAnimated;
        private readonly ICommand _setMovingStatic;
        private readonly ICommand _setMovingAnimated;

        public ControllerMouse(ZeldaGame zeldaGame)
        {
            _quit = new CommandQuit(zeldaGame);
            _setFixedStatic = new CommandSetFixedStatic(zeldaGame);
            _setFixedAnimated = new CommandSetFixedAnimated(zeldaGame);
            _setMovingStatic = new CommandSetMovingStatic(zeldaGame);
            _setMovingAnimated = new CommandSetMovingAnimated(zeldaGame);
            _zeldaGame = zeldaGame;
        }

        public void Update()
        {
            Rectangle viewportBounds = _zeldaGame.GraphicsDevice.Viewport.Bounds;
            viewportBounds.X = 0;
            viewportBounds.Y = 0;
            MouseState mouseState = Mouse.GetState();

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                _quit.Execute();
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!viewportBounds.Contains(mouseState.X, mouseState.Y))
                {
                    return;
                }

                if (mouseState.X < viewportBounds.Width / 2)
                {
                    if (mouseState.Y < viewportBounds.Height / 2)
                    {
                        _setFixedStatic.Execute();
                    }
                    else
                    {
                        _setMovingStatic.Execute();
                    }
                }
                else
                {
                    if (mouseState.Y < viewportBounds.Height / 2)
                    {
                        _setFixedAnimated.Execute();
                    }
                    else
                    {
                        _setMovingAnimated.Execute();
                    }
                }
            }
        }
    }
}
