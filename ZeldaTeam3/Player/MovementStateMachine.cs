using System;
using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    class MovementStateMachine : IMoveable
    {
        // Frame delay: 1/60 fps = ~1/60s
        private const int FrameDelay = 1;
        public Direction Facing { get; private set; } = Direction.Right;
        public Direction Moving { get; private set; } = Direction.Right;
        public bool Idling { get; private set; } = true;
        public Vector2 Location { get; private set; }

        private int _framesDelayed;

        public MovementStateMachine(Vector2 location)
        {
            Location = location;
        }

        private void AdvanceLocation()
        {
            switch (Moving)
            {
                case Direction.Up:
                    Location = new Vector2(Location.X, Location.Y - 1);
                    break;
                case Direction.Down:
                    Location = new Vector2(Location.X, Location.Y + 1);
                    break;
                case Direction.Left:
                    Location = new Vector2(Location.X - 1, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Vector2(Location.X + 1, Location.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int FramesDelayed
        {
            get => _framesDelayed;
            set
            {
                if (Idling) return;
                _framesDelayed = value;

                if (_framesDelayed != FrameDelay) return;
                AdvanceLocation();
                _framesDelayed = 0;
            }
        }

        public void FaceUp()
        {
            Facing = Direction.Up;
        }

        public void FaceDown()
        {
            Facing = Direction.Down;
        }

        public void FaceLeft()
        {
            Facing = Direction.Left;
        }

        public void FaceRight()
        {
            Facing = Direction.Right;
        }

        public void MoveUp()
        {
            Moving = Direction.Up;
            Idling = false;
        }

        public void MoveDown()
        {
            Moving = Direction.Down;
            Idling = false;
        }

        public void MoveLeft()
        {
            Moving = Direction.Left;
            Idling = false;
        }

        public void MoveRight()
        {
            Moving = Direction.Right;
            Idling = false;
        }

        public void Idle()
        {
            Idling = true;
        }

        public void Update()
        {
            FramesDelayed++;
            Idling = true;
        }
    }
}
