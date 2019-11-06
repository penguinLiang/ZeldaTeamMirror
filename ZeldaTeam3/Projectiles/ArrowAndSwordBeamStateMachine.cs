using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    public class ArrowAndSwordBeamStateMachine
    {
        private const int MaxFramesAway = 100;

        private readonly Direction _direction;
        private int _currentFrame;
        private int _distancePerFrame;
        private Point _location;

        public Point Location => _location;
        public Rectangle Bounds { get; private set; }

        public ArrowAndSwordBeamStateMachine(Point location, Direction direction, int speed)
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
            _distancePerFrame = speed;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public void ClearBounds()
        {
            Bounds = Rectangle.Empty;
        }

        public void Update()
        {
            if (_currentFrame++ >= MaxFramesAway) return;

            switch (_direction)
            {
                case Direction.Up:
                    _location.Y -= _distancePerFrame;
                    break;
                case Direction.Down:
                    _location.Y += _distancePerFrame;
                    break;
                case Direction.Left:
                    _location.X -= _distancePerFrame;
                    break;
                case Direction.Right:
                    _location.X += _distancePerFrame;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Bounds = new Rectangle(_location, Bounds.Size);
        }
    }
}
