using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class SpriteMovingAnimated : ISprite
    {
        // Defaults to two frame draws per second, 30 / 60 FPS
        public int FrameDelay { get; set; } = 30;
        // Defaults to moving roughly three times per second, 20 / 60 FPS
        public int UpdateDelay { get; set; } = 20;

        private readonly Texture2D _spriteSheet;
        private readonly Rectangle[] _frameBoundingBoxes;
        private int _currentFrame;
        private int _framesDelayed;
        private int _velocity = 2;
        private int _xOffset;
        private int _updatesDelayed;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                if (_framesDelayed++ != FrameDelay) return;
                _currentFrame = value % _frameBoundingBoxes.Length;
                _framesDelayed = 0;
            }
        }

        public SpriteMovingAnimated(Texture2D spriteSheet, Rectangle[] frameBoundingBoxes)
        {
            _spriteSheet = spriteSheet;
            _frameBoundingBoxes = frameBoundingBoxes;
        }

        public void Update()
        {
            CurrentFrame++;
            if (_updatesDelayed++ != UpdateDelay) return;
            if (_xOffset == 20 || _xOffset == -20)
            {
                _velocity *= -1;
            }
            _xOffset += _velocity;
            _updatesDelayed = 0;
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Vector2 offsetLocation = location;
            offsetLocation.X += _xOffset;

            if (_velocity > 0)
            {
                spriteBatch.Draw(_spriteSheet, offsetLocation, _frameBoundingBoxes[_currentFrame], Color.White);
            }
            else
            {
                spriteBatch.Draw(_spriteSheet, new Rectangle(offsetLocation.ToPoint(), _frameBoundingBoxes[_currentFrame].Size),
                    _frameBoundingBoxes[_currentFrame], Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally,
                    1f);
            }

        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void PauseAnimation()
        {
            throw new NotImplementedException();
        }

        public void PlayAnimation()
        {
            throw new NotImplementedException();
        }
    }
}
