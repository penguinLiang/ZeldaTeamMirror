using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    internal class PlayerBoomerang : ICollideable, IDrawable
    {
        private const int ReturnDistance = 100;
        private const int DistancePerFrame = 5;

        private Vector2 _location;
        private readonly ISprite _sprite;

        private int _currentDistanceAway;
        private Direction _direction;
        private Rectangle _bounds;

        public PlayerBoomerang(Point location, Direction direction)
        {
            _direction = direction;
            _bounds = new Rectangle(location.X, location.Y, 8, 8);
            _location = location.ToVector2();
            _sprite = ProjectileSpriteFactory.Instance.CreateThrownBoomerang();
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

        public bool CollidesWith(Rectangle rectangle)
        {
            return _bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            Type type = enemy.GetType();
            if (type.Equals(typeof(Enemies.Keese)) || type.Equals(typeof(Enemies.Gel)) || type.Equals(typeof(Enemies.OldMan)))
            {
                return new Commands.SpawnableDamage(enemy, 1);
            }
            else if (type.Equals(typeof(Enemies.Stalfos)) || type.Equals(typeof(Enemies.Goriya)) || type.Equals(typeof(Enemies.WallMaster)))
            {
                return new Commands.MoveableHalt(enemy);
            }
            else if (type.Equals(typeof(Enemies.Aquamentus)) || type.Equals(typeof(Enemies.Trap)))
            {
                return Commands.NoOp.Instance;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return Commands.NoOp.Instance;
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
            _sprite.Draw(_location);
        }
    }
}
