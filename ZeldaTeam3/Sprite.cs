using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    internal class Sprite : ISprite
    {
        // 20/60 fps
        private const int DefaultFrameDelay = 20;
        // 4/60 fps, 1/5 of frame delay
        private const int DefaultPaletteShiftDelay = 4;
        private const int PaletteShiftCycles = 4;

        private readonly Texture2D _spriteSheet;
        private readonly int _width;
        private readonly int _height;
        private readonly int _frameCount;
        private readonly Point _sourceOffset;
        private readonly int _frameDelay;
        private readonly int _paletteHeight;
        private readonly int _totalPaletteCount;
        private readonly int _paletteShiftDelay;
        private int _currentFrame;
        private int _currentPaletteRow;
        private int _framesDelayed;
        private int _paletteShiftsDelayed;
        private int _paletteCyclesShifted;
        private bool _visible = true;
        private bool _playing = true;
        private bool _paletteShifting;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                if (!_playing || _frameCount == 0 || _framesDelayed++ != _frameDelay) return;
                _currentFrame = value % _frameCount;
                _framesDelayed = 0;
            }
        }

        private int CurrentPaletteRow
        {
            get => _currentPaletteRow;
            set
            {
                if (!_paletteShifting || _totalPaletteCount == 0 || _paletteShiftsDelayed++ != _paletteShiftDelay) return;
                _currentPaletteRow = value % _totalPaletteCount;
                _paletteShiftsDelayed = 0;

                if (_currentPaletteRow != 0 || _paletteCyclesShifted++ != PaletteShiftCycles) return;
                _paletteShifting = false;
                _paletteCyclesShifted = 0;
            }
        }

        private int SourceX => _sourceOffset.X + _currentFrame * _width;
        private int SourceY => _sourceOffset.Y + _currentPaletteRow * _paletteHeight;

        public Sprite(Texture2D spriteSheet, int width, int height, int frameCount, Point sourceOffset, int frameDelay = DefaultFrameDelay, int paletteHeight = 0, int totalPaletteCount = 0, int paletteShiftDelay = DefaultPaletteShiftDelay)
        {
            _width = width;
            _height = height;
            _frameCount = frameCount;
            _frameDelay = frameDelay;
            _spriteSheet = spriteSheet;
            _paletteHeight = paletteHeight;
            _totalPaletteCount = totalPaletteCount;
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

        public void PaletteShift()
        {
            _paletteShifting = true;
        }
    }
}