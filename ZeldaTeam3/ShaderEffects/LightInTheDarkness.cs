using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.ShaderEffects
{
    public class LightInTheDarkness
    {
        public static Effect ShaderEffect { set; private get; }
        public static SpriteBatch SpriteBatch { set; private get; }
        public static GraphicsDevice GraphicsDevice { set; private get; }

        private readonly RenderTarget2D _alphaMaskTarget;
        private readonly RenderTarget2D _overlayTarget;

        public LightInTheDarkness()
        {
            if (ShaderEffect == null || SpriteBatch == null || GraphicsDevice == null)
            {
                throw new ArgumentNullException();
            }
            var height = GraphicsDevice.Viewport.Height;
            var width = GraphicsDevice.Viewport.Width;
            _alphaMaskTarget = new RenderTarget2D(GraphicsDevice, width, height, false,
                GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            _overlayTarget = new RenderTarget2D(GraphicsDevice, width, height, false,
                GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
        }

        private static Vector2 VectorizeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.Left:
                    return new Vector2(-1, 0);
                case Direction.Right:
                    return new Vector2(1, 0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Track(Vector2 location, Direction direction)
        {
            ShaderEffect.Parameters["LightCoord"].SetValue(location * 2.0f);
            ShaderEffect.Parameters["LightDirection"].SetValue(VectorizeDirection(direction));
        }

        public Texture2D Overlay(Action render, Vector2 offset)
        {
            AlphaPassMask.Enabled = true;
            GraphicsDevice.SetRenderTarget(_alphaMaskTarget);
            GraphicsDevice.Clear(Color.TransparentBlack);
            render();

            AlphaPassMask.Enabled = false;
            GraphicsDevice.SetRenderTarget(_overlayTarget);
            SpriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.Clear(Color.TransparentBlack);
            ShaderEffect.Techniques["RayCast"].Passes[0].Apply();
            SpriteBatch.Draw(_alphaMaskTarget, offset,
                new Rectangle(offset.ToPoint(), _overlayTarget.Bounds.Size - offset.ToPoint()), Color.White);
            SpriteBatch.End();

            return _overlayTarget;
        }
    }
}
