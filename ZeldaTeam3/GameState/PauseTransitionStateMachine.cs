using System;
using Zelda.Pause;

namespace Zelda.GameState
{
    public class PauseTransitionStateMachine : IUpdatable
    {
        private const int TransitionFrames = 60;
        private static readonly int PauseStep = (int)Math.Ceiling(PauseSpriteFactory.ScreenHeight / (float)TransitionFrames);

        private readonly FrameDelay _pauseTransitionDelay = new FrameDelay(TransitionFrames, true);
        private int _pauseAnimationFrame;

        public float YOffset => _pauseAnimationFrame * PauseStep;
        public PauseState State { get; private set; } = PauseState.Playing;

        private int FrameVelocity()
        {
            switch (State)
            {
                case PauseState.Pausing:
                    return 1;
                case PauseState.Unpausing:
                    return -1;
                case PauseState.Playing:
                case PauseState.Paused:
                case PauseState.Unpaused:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Pause()
        {
            if (State != PauseState.Playing) return;
            State = PauseState.Pausing;
            _pauseTransitionDelay.Resume();
        }

        public void Resume()
        {
            if (State != PauseState.Paused) return;
            State = PauseState.Unpausing;
            _pauseTransitionDelay.Resume();
        }

        public void Play()
        {
            if (State != PauseState.Unpaused) return;
            State = PauseState.Playing;
        }

        public void Update()
        {
            if (State == PauseState.Playing) return;

            _pauseTransitionDelay.Update();
            if (_pauseTransitionDelay.Delayed)
            {
                _pauseAnimationFrame += FrameVelocity();
                return;
            }

            _pauseTransitionDelay.Pause();
            if (State == PauseState.Pausing)
            {
                State = PauseState.Paused;
            }
            if (State == PauseState.Unpausing)
            {
                State = PauseState.Unpaused;
            }
        }
    }
}
