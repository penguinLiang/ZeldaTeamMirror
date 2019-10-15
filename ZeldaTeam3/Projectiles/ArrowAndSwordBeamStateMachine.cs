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
        private int damage;
        private Point _location;
        private Rectangle _bounds;

        public Point Location => _location;

        public ArrowAndSwordBeamStateMachine(Point location, Direction direction, int damage)
        {
            _currentFrame = 0;
            _direction = direction;
            _location = location;
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    _bounds = new Rectangle(location.X + 4, location.Y, 8, 16);
                    break;
                case Direction.Left:
                case Direction.Right:
                    _bounds = new Rectangle(location.X, location.Y + 4, 16, 8);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _bounds.Intersects(rectangle);
        }

        public void ClearBounds()
        {
            _bounds = new Rectangle(0, 0, 0, 0);
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
        }
    }
}
