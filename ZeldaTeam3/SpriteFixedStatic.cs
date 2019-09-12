using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class SpriteFixedStatic : ISprite
    {
        private readonly Texture2D _spriteSheet;
        private readonly Rectangle _boundingBox;

        public SpriteFixedStatic(Texture2D spriteSheet, Rectangle boundingBox)
        {
            _spriteSheet = spriteSheet;
            _boundingBox = boundingBox;
        }

        public void Update()
        {
            // No op, no movement or animation required
        }

        public void LoadContent()
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(_spriteSheet, location, _boundingBox, Color.White);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void PauseAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}
