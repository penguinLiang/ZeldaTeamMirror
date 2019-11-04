using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    public class ArrowAndSwordBeamStateMachine
    {
        private const int MaxFramesAway = 48;
        private const int DistancePerFrame = 4;

        private readonly Direction _direction;
        private int _currentFrame;
        private Point _location;

        public Point Location => _location;
        public Rectangle Bounds { get; private set; }

        public ArrowAndSwordBeamStateMachine(Point location, Direction direction)
        {
            _currentFrame = 0;
            _direction = direction;
            _location = location;
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    Bounds = new Rectangle(location.X + 4, location.Y, 8, 16);
                    break;
                case Direction.Left:
                case Direction.Right:
                    Bounds = new Rectangle(location.X, location.Y + 4, 16, 8);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return Bounds.Intersects(rectangle);
        }

        public void ClearBounds()
        {
            Bounds = new Rectangle(0, 0, 0, 0);
        }

        public void Update()
        {
            if (_currentFrame++ >= MaxFramesAway) return;

            switch (_direction)
            {
                case Direction.Up:
                    _location.Y -= DistancePerFrame;
                    break;
                case Direction.Down:
                    _location.Y += DistancePerFrame;
                    break;
                case Direction.Left:
                    _location.X -= DistancePerFrame;
                    break;
                case Direction.Right:
                    _location.X += DistancePerFrame;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Bounds = new Rectangle(_location, Bounds.Size);
        }
    }
}
