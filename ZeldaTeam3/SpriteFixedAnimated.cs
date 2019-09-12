using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class SpriteFixedAnimated : ISprite
    {
        // Defaults to four frame draws per second, 15 / 60 FPS
        public int FrameDelay { get; set; } = 15;

        private readonly Texture2D _spriteSheet;
        private readonly Rectangle[] _frameBoundingBoxes;
        private int _currentFrame;
        private int _framesDelayed;

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

        public SpriteFixedAnimated(Texture2D spriteSheet, Rectangle[] frameBoundingBoxes)
        {
            _spriteSheet = spriteSheet;
            _frameBoundingBoxes = frameBoundingBoxes;
        }

        public void Update()
        {
            CurrentFrame++;
        }

        public void LoadContent()
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(_spriteSheet, location, _frameBoundingBoxes[_currentFrame], Color.White);
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