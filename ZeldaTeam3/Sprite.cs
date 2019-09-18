using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class Sprite : ISprite
    {
        // 20/60 fps
        private const int DefaultFrameDelay = 20;
        // 5/60 fps, 1/4 of frame delay
        private const int DefaultPaletteShiftDelay = 5;

        private readonly Texture2D _spriteSheet;
        private readonly int _width;
        private readonly int _height;
        private readonly int _frameCount;
        private readonly Point _sourceOffset;
        private readonly int _frameDelay;
        private readonly int _paletteRows;
        private readonly int _paletteRowCount;
        private readonly int _paletteShiftDelay;
        private int _currentFrame;
        private int _currentPaletteRow;
        private int _framesDelayed;
        private int _paletteShiftsDelayed;
        private bool _visible = true;
        private bool _playing = true;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                if (_frameCount == 0 || _framesDelayed++ != _frameDelay || !_playing) return;
                _currentFrame = value % _frameCount;
                _framesDelayed = 0;
            }
        }

        private int CurrentPaletteRow
        {
            get => _currentPaletteRow;
            set
            {
                if (_paletteRows == 0 || _paletteShiftsDelayed++ != _paletteShiftDelay || !_playing) return;
                _currentPaletteRow = value % _paletteRows;
                _paletteShiftsDelayed = 0;
            }
        }

        private int SourceX => _sourceOffset.X + _currentFrame * _width;
        private int SourceY => _sourceOffset.Y + _height * _currentPaletteRow * _paletteRowCount;


        public Sprite(Texture2D spriteSheet, int width, int height, int frameCount, Point sourceOffset, int frameDelay = DefaultFrameDelay, int paletteRowCount = 0, int paletteRows = 0, int paletteShiftDelay = DefaultPaletteShiftDelay)
        {
            _width = width;
            _height = height;
            _frameCount = frameCount;
            _frameDelay = frameDelay;
            _spriteSheet = spriteSheet;
            _paletteRowCount = paletteRowCount;
            _paletteRows = paletteRows;
            _paletteShiftDelay = paletteShiftDelay;
            _sourceOffset = sourceOffset;
        }

        public void Update()
        {
            CurrentFrame++;
            CurrentPaletteRow++;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (!_visible) return;
            spriteBatch.Draw(_spriteSheet, location, new Rectangle(SourceX, SourceY, _width, _height), Color.White);
        }

        public void Show()
        {
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }

        public void PauseAnimation()
        {
            _playing = false;
        }

        public void PlayAnimation()
        {
            _playing = true;
        }
    }
}