using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    internal class Sprite : ISprite
    {
        public static SpriteBatch SpriteBatch { private get; set; }

        public bool AnimationFinished { get; private set; }

        // 20/60 fps
        private const int DefaultFrameDelay = 20;
        // 4/60 fps, 1/5 of frame delay
        private const int DefaultPaletteShiftDelay = 4;
        private const int PaletteShiftCycles = 4;

        /* Each sprite sheet follows a convention such that row dimensions may vary,
           but each column has the same width and height. */
        private readonly Texture2D _spriteSheet;
        private readonly int _width;
        private readonly int _height;

        /* A source offset must be provided since the dimensions of each row may vary,
           and some sheets are single frames and not contiguous. */
        private readonly Point _sourceOffset;

        /* The number of frames delayed between draws for an animation can vary per sprite and
           the number of frames in an animation cannot be automatically determined since
           row dimensions vary. */
        private readonly FrameDelay _frameDelay;
        private readonly int _frameCount;
        private int _currentFrame;

        /* In addition to an animation consisting of frames in a row, there is also a concurrent
           animation that can occur that shifts the vertical offset by a group of rows called a palette.
           This palette is largely used for color shifting while maintaining the current frame appearance
           in the animation. */
        private readonly FrameDelay _paletteShiftDelay;
        private readonly int _paletteHeight;
        private readonly int _totalPaletteCount;
        private int _currentPaletteRow;
        private int _paletteCyclesShifted;

        private bool _visible = true;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                if (_frameCount == 0 || _frameDelay.Delayed) return;
                _currentFrame = value % _frameCount;
                if (_currentFrame == 0) AnimationFinished = true;
            }
        }

        private int CurrentPaletteRow
        {
            get => _currentPaletteRow;
            set
            {
                if (_totalPaletteCount == 0 || _paletteShiftDelay.Delayed) return;
                _currentPaletteRow = value % _totalPaletteCount;

                if (_currentPaletteRow != 0 || _paletteCyclesShifted++ != PaletteShiftCycles) return;
                _paletteShiftDelay.Pause();
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
            _spriteSheet = spriteSheet;
            _paletteHeight = paletteHeight;
            _totalPaletteCount = totalPaletteCount;
            _sourceOffset = sourceOffset;
            _frameDelay = new FrameDelay(frameDelay);
            _paletteShiftDelay = new FrameDelay(paletteShiftDelay);
            _paletteShiftDelay.Pause();
        }

        public void Update()
        {
            _frameDelay.Update();
            _paletteShiftDelay.Update();
            CurrentFrame++;
            CurrentPaletteRow++;
        }

        public void Draw(Vector2 location)
        {
            if (!_visible) return;
            SpriteBatch.Draw(_spriteSheet, location, new Rectangle(SourceX, SourceY, _width, _height), Color.White);
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
            _frameDelay.Pause();
        }

        public void PlayAnimation()
        {
            _frameDelay.Resume();
        }

        public void PaletteShift()
        {
            _paletteShiftDelay.Resume();
        }
    }
}
 