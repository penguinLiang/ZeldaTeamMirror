using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Projectiles
{
    public class ThrownBoomerang : IDrawable
    {
        private const int ReturnDistance = 100;
        private const int DistancePerFrame = 5;

        private Vector2 _location;
        private readonly SpriteBatch _spriteBatch;
        private readonly ISprite _sprite;

        private int _currentDistanceAway;
        private Direction _direction;

        public ThrownBoomerang(SpriteBatch spriteBatch, Vector2 location, Direction direction)
        {
            _direction = direction;
            _location = location;
            _sprite = ProjectileSpriteFactory.Instance.CreateThrownBoomerang();
            _spriteBatch = spriteBatch;
            _currentDistanceAway = 0;
        }

        private void UpdateFlippedDirection()
        {
            if (_currentDistanceAway != ReturnDistance) return;

            switch (_direction)
            {
                case Direction.Up:
                    _direction = Direction.Down;
                    break;
                case Direction.Down:
                    _direction = Direction.Up;
                    break;
                case Direction.Left:
                    _direction = Direction.Right;
                    break;
                case Direction.Right:
                    _direction = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            if (_currentDistanceAway == ReturnDistance * 2)
            {
                _sprite.Hide();
            }
            UpdateFlippedDirection();

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
            _currentDistanceAway += DistancePerFrame;

            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, _location);
        }
    }
}
