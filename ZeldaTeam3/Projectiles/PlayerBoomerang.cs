using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Zelda.Commands;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    internal class PlayerBoomerang : IProjectile
    {
        public Rectangle Bounds => Halted ? Rectangle.Empty : new Rectangle(_location.ToPoint(), new Point(8, 8));

        private const int ReturnDistance = 60;
        private const int DistancePerFrame = 3;

        private Vector2 _location;
        private readonly ISprite _sprite = ProjectileSpriteFactory.Instance.CreateThrownBoomerang();

        private Vector2? _collisionLocation;
        private ISprite _collision;
        private bool _collided;

        private int _currentDistanceAway;
        private readonly IPlayer _player;
        private readonly Direction _direction;
        public bool Halted { get; private set; }

        private readonly SoundEffectInstance _soundEffect;

        public PlayerBoomerang(IPlayer player, Point location, Direction direction)
        {
            _soundEffect = SoundEffectManager.Instance.PlayBoomerang();
            _player = player;
            _direction = direction;
            _location = location.ToVector2();
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            Halted = true;
            player.Inventory.AddSecondaryItem(Secondary.Boomerang);
            _soundEffect.Stop();
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            _collisionLocation = null;
            switch (enemy)
            {
                case Keese _:
                case Gel _:
                case OldMan _:
                    return new SpawnableDamage(enemy, 1);
                case Stalfos _:
                case Goriya _:
                case WallMaster _:
                    return new EnemyStun(enemy);
                case Aquamentus _:
                case Trap _:
                    return NoOp.Instance;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void LinearUpdate()
        {
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
        }

        private void TrackingUpdate()
        {
            _soundEffect.Resume();
            if (_location.X < _player.Location.X + DistancePerFrame)
            {
                _location.X += DistancePerFrame;
            }
            else if (_location.X > _player.Location.X - DistancePerFrame)
            {
                _location.X -= DistancePerFrame;
            }
            if (_location.Y < _player.Location.Y + DistancePerFrame)
            {
                _location.Y += DistancePerFrame;
            }
            else if (_location.Y > _player.Location.Y - DistancePerFrame)
            {
                _location.Y -= DistancePerFrame;
            }
        }

        public void Update()
        {
            if (_collided && _collisionLocation != _location)
            {
                _collision = ProjectileSpriteFactory.Instance.CreateBoomerangCollision();
            }

            if (_currentDistanceAway == ReturnDistance || _collided)
            {
                TrackingUpdate();
            }
            else
            {
                LinearUpdate();
            }

            _sprite.Update();
        }

        public void Halt()
        {
            _soundEffect.Pause();
            if (_collided) return;

            _collided = true;
            _collisionLocation = _location;
        }

        public void Reflect(List<Rectangle> orderedBounds)
        {
            //NO-OP
        }

        public void Draw()
        {
            if (_collisionLocation != null) _collision?.Draw((Vector2)_collisionLocation);
            _sprite.Draw(_location);
        }
    }
}
