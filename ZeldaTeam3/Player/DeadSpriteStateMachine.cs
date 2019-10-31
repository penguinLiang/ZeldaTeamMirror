using System;

namespace Zelda.Player
{
    /*
     * Manages the Sprite used in the three stages of Link's death:
     *  - Spinning
     *  - Gray
     *  - Sparkle
     */
    internal class DeadSpriteStateMachine : IUpdatable
    {
        private const int MaxSpinCycles = 5;
        private const int GrayFrame = 1;
        private const int GrayToSparklesFrame = 60;
        private const int SparkleToHideFrame = 200;
        private readonly FrameDelay _spinFrameDelay = new FrameDelay(4);

        public ISprite Sprite { get; private set; }

        private Direction _facing;
        private int _spinCycles;
        private int _transitionFrames;

        public DeadSpriteStateMachine()
        {
            _spinFrameDelay.Pause();
        }

        public void Aim(Direction direction)
        {
            _facing = direction;
        }

        public void Spin()
        {
            if (_spinCycles == MaxSpinCycles || _spinFrameDelay.Delayed) return;
            switch (_facing)
            {
                case Direction.Left:
                    _facing = Direction.Up;
                    break;
                case Direction.Up:
                    _facing = Direction.Right;
                    break;
                case Direction.Right:
                    _facing = Direction.Down;
                    _spinCycles++;
                    break;
                case Direction.Down:
                    _facing = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Sprite = LinkSpriteFactory.Instance.CreateNoWeapon(_facing);
            Sprite.PauseAnimation();
        }

        public void PostSpin()
        {
            if (_spinCycles < MaxSpinCycles) return;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_transitionFrames++)
            {
                case GrayFrame:
                    Sprite = LinkSpriteFactory.Instance.CreateDeadLink();
                    break;
                case GrayToSparklesFrame:
                    Sprite = LinkSpriteFactory.Instance.CreateLinkDeathSparkle();
                    break;
                case SparkleToHideFrame:
                    Sprite.Hide();
                    break;
            }
        }

        public void Update()
        {
            _spinFrameDelay.Update();
            _spinFrameDelay.Resume();
            Spin();
            PostSpin();
            Sprite.Update();
        }
    }
}
