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
        private readonly FrameDelay _movementDelay = new FrameDelay(1);
        private readonly FrameDelay _disableKnockbackDelay = new FrameDelay(10);

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
            _disableKnockbackDelay.Pause();
        }

        private void AdvanceLocation()
        {
            if (Idling || _halted || _movementDelay.Delayed) return;
            _lastLocation = Location;

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

        public void TeleportToEntrance(Direction entranceDirection)
        {
            Idling = true;
            _halted = false;
            switch (entranceDirection)
            {
                case Direction.Up:
                    _lastLocation = Location = new Point(16 * 8, 16 * 2);
                    Facing = _moving = Direction.Down;
                    break;
                case Direction.Down:
                    _lastLocation = Location = new Point(16 * 8, 16 * 8);
                    Facing = _moving = Direction.Up;
                    break;
                case Direction.Left:
                    _lastLocation = Location = new Point(16 * 2,16 * 5);
                    Facing = _moving = Direction.Right;
                    break;
                case Direction.Right:
                    _lastLocation = Location = new Point(16 * 13,16 * 5);
                    Facing = _moving = Direction.Left;
                    break;
                case Direction.DownFromDungeon:
                    _lastLocation = Location = new Point(16 * 3, 16 * 3);
                    Facing = _moving = Direction.Down;
                    break;
                case Direction.UpFromBasement:
                    _lastLocation = Location = new Point(16 * 6, 16 * 7);
                    Facing = _moving = Direction.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
