using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.ShaderEffects
{
    public class AlphaPassMask : ISprite
    {
        public static Texture2D OpaqueMaskTexture { private get; set; }
        public static Texture2D TransparentMaskTexture { private get; set; }
        public static SpriteBatch SpriteBatch { private get; set; }
        public static bool Enabled { private get; set; }

        private readonly int _width;
        private readonly int _height;
        private readonly int _yOffset;
        private readonly ISprite _sprite;
        private readonly bool _opaque;
        private bool _hidden;

        public AlphaPassMask(int width, int height, int yOffset = 0, bool opaque = true)
        {
            _width = width;
            _height = height;
            _yOffset = yOffset;
            _opaque = opaque;
        }

        public AlphaPassMask(ISprite sprite, bool opaque)
        {
            _sprite = sprite;
            _opaque = opaque;
        }

        public bool AnimationFinished => _sprite.AnimationFinished;
        public int Height => _sprite?.Height ?? _height;
        public int Width => _sprite?.Width ?? _width;

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw(Vector2 location)
        {
            if (!Enabled)
            {
                _sprite?.Draw(location);
            }
            else
            {
                if (_hidden) return;

                SpriteBatch.Draw(_opaque ? OpaqueMaskTexture : TransparentMaskTexture, location + new Vector2(0, _yOffset), null, Color.Black, 0f, Vector2.Zero,
                    new Vector2(Width, Height), SpriteEffects.None, 0f);
            }
        }

        public void Hide()
        {
            _hidden = true;
        }

        public void PauseAnimation()
        {
            _sprite?.PauseAnimation();
        }

        public void PlayAnimation()
        {
            _sprite?.PlayAnimation();
        }

        public void PaletteShift()
        {
            _sprite?.PaletteShift();
        }
    }
}
