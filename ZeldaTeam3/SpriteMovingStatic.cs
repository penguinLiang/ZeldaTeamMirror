using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class SpriteMovingStatic : ISprite
    {
        // Defaults to moving roughly every update
        public int UpdateDelay { get; set; } = 0;

        private readonly Texture2D _spriteSheet;
        private readonly Rectangle _boundingBox;
        private readonly int _maxYOffset;
        private int _velocity = -2;
        private int _yOffset;
        private int _updatesDelayed;

        public SpriteMovingStatic(Texture2D spriteSheet, Rectangle boundingBox, int maxYOffset)
        {
            _spriteSheet = spriteSheet;
            _boundingBox = boundingBox;
            _maxYOffset = maxYOffset;
        }

        public void Update()
        {
            if (_updatesDelayed++ != UpdateDelay) return;
            if (_yOffset >= _maxYOffset || _yOffset <= 0)
            {
                _velocity *= -1;
            }
            _yOffset += _velocity;
            _updatesDelayed = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(_spriteSheet, new Vector2(location.X, location.Y - _yOffset), _boundingBox, Color.White);
        }
    }
}
