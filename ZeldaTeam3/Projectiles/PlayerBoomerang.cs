using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Enemies;

namespace Zelda.Projectiles
{
    internal class PlayerBoomerang : IProjectile, IDrawable
    {
        private const int ReturnDistance = 80;
        private const int DistancePerFrame = 5;

        private Vector2 _location;
        private readonly ISprite _sprite;

        private int _currentDistanceAway;
        private Direction _direction;
        public Rectangle Bounds { get; }
        public bool Halted { get; set; }

        public PlayerBoomerang(Point location, Direction direction)
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
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            switch (enemy)
            {
                case Keese _:
                case Gel _:
                case OldMan _:
                    return new Commands.SpawnableDamage(enemy);
                case Stalfos _:
                case Goriya _:
                case WallMaster _:
                    return new Commands.MoveableHalt(enemy);
                case Aquamentus _:
                case Trap _:
                    return Commands.NoOp.Instance;
                default:
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

        public void Knockback() {
            //no op
        }

        public void Halt() {
            Halted = true;
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
