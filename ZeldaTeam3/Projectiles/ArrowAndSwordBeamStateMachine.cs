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
        private Vector2 _location;

        public Vector2 Location => _location;

        public ArrowAndSwordBeamStateMachine(Vector2 location, Direction direction)
        {
            _currentFrame = 0;
            _direction = direction;
            _location = location;
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
