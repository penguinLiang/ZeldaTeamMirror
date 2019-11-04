using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    internal class GoriyaBoomerang : IProjectile
    {
        private const int ReturnDistance = 60;
        private const int DistancePerFrame = 4;

        private Vector2 _location;
        private readonly ISprite _sprite;

        private int _currentDistanceAway;
        private Direction _direction;
        public Rectangle Bounds { get; private set; }
        public bool Halted { get; set; }


        public GoriyaBoomerang(Point location, Direction direction)
        {
            _direction = direction;
            Bounds = new Rectangle(location.X, location.Y, 8, 8);
            _location = location.ToVector2();
            _sprite = ProjectileSpriteFactory.Instance.CreateThrownBoomerang();
            _currentDistanceAway = 0;
            Halted = false;
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
            return Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            Halt();
            Bounds = new Rectangle(0, 0, 0, 0);
            return new Commands.SpawnableDamage(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
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
            Bounds = new Rectangle(_location.ToPoint(), Bounds.Size);

            _sprite.Update();
        }

        public void Halt() {
            Halted = true;
        }

        public void Knockback() {
            //no op 
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
