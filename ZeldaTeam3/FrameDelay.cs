namespace Zelda
{
    internal class FrameDelay
    {
        private readonly int _frames;
        private bool _paused;
        private int _framesDelayed;

        public bool Delayed => _framesDelayed != 0 || _paused;

        public FrameDelay(int frames)
        {
            _frames = frames;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void Update()
        {
            if (!_paused && _framesDelayed++ == _frames) _framesDelayed = 0;
        }
    }
}
