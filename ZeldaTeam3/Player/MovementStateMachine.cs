using System;
using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    internal class MovementStateMachine : IHaltable
    {
        private readonly FrameDelay _movementDelay = new FrameDelay(1);
        private readonly FrameDelay _disableKnockbackDelay = new FrameDelay(20);

        public Direction Facing { get; private set; } = Direction.Right;
        public bool Idling { get; private set; } = true;
        public bool Knockedback { get; private set; }
        public Point Location { get; private set; }

        private Direction _moving;

        public MovementStateMachine(Point location)
        {
            Location = location;
            _disableKnockbackDelay.Pause();
        }

        private void AdvanceLocation()
        {
            if (Idling || _movementDelay.Delayed) return;

            switch (_moving)
            {
                case Direction.Up:
                    Location = new Point(Location.X, Location.Y - 2);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + 2);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - 2, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + 2, Location.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Move(Direction direction)
        {
            if (Knockedback) return;

            Facing = _moving = direction;
            Idling = false;
        }

        public void Knockback()
        {
            Idling = false;
            Knockedback = true;

            switch (Facing)
            {
                case Direction.Up:
                    _moving = Direction.Down;
                    break;
                case Direction.Down:
                    _moving = Direction.Up;
                    break;
                case Direction.Left:
                    _moving = Direction.Right;
                    break;
                case Direction.Right:
                    _moving = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _disableKnockbackDelay.Resume();
        }

        public void Halt()
        {
            Idling = true;
        }

        public void Update()
        {
            _disableKnockbackDelay.Update();
            _movementDelay.Update();
            AdvanceLocation();

            if (!Knockedback) Idling = true;

            if (_disableKnockbackDelay.Delayed) return;
            _disableKnockbackDelay.Pause();
            Knockedback = false;
            _moving = Facing;
        }
    }
}
