using System;
using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    /*
     * Manages Link's
     *  - Facing/movement direction
     *  - Location from movement/teleportation
     *  - Whether or not idle
     */
    internal class MovementStateMachine : IHaltable, IUpdatable
    {
        private const int Speed = 2;
        private const int TileSize = 8;
        private const int AlignThreshold = 3;

        private readonly FrameDelay _movementDelay = new FrameDelay(1);
        private readonly FrameDelay _disableKnockbackDelay = new FrameDelay(10, true);

        public Direction Facing { get; private set; } = Direction.Right;
        public bool Idling { get; private set; } = true;
        public bool Knockedback { get; private set; }
        public Point Location { get; private set; }

        private Direction _moving;
        private bool _halted;
        private Point _lastLocation;

        public MovementStateMachine(Point location)
        {
            Location = _lastLocation = location;
        }

        private void AdvanceLocation()
        {
            if (Idling || _halted || _movementDelay.Delayed) return;
            _lastLocation = Location;
            AlignMovement(_moving);
        }

        private void AlignMovement(Direction direction)
        {
            if (direction == Direction.Down || direction == Direction.Up)
            {
                var distance = Location.X % TileSize;
                if (distance == 0)
                {
                    Location += new Point(0, direction == Direction.Down ? Speed : -Speed);
                }
                else
                {
                    Location += new Point(distance > AlignThreshold ? Speed : -Speed, 0);
                }
            }
            else
            {
                var distance = Location.Y % TileSize;
                if (distance == 0)
                {
                    Location += new Point(direction == Direction.Left ? -Speed : Speed, 0);
                }
                else
                {
                    Location += new Point(0, distance > AlignThreshold ? Speed : -Speed);
                }
            }
        }

        public void Move(Direction direction)
        {
            if (Knockedback || _halted) return;

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
            _halted = true;
            Idling = true;
            Location = _lastLocation;
        }

        public void Teleport(Point location, Direction facing)
        {
            Idling = true;
            _halted = false;
            Facing = _moving = facing;
            Location = _lastLocation = location;
        }

        public void Update()
        {
            _disableKnockbackDelay.Update();
            _movementDelay.Update();
            AdvanceLocation();

            if (!Knockedback) Idling = true;
            _halted = false;

            if (_disableKnockbackDelay.Delayed) return;
            _disableKnockbackDelay.Pause();
            Knockedback = false;
            _moving = Facing;
        }
    }
}
