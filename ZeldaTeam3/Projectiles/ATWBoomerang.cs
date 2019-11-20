using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Zelda.Commands;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    internal class ATWBoomerang : IProjectile
    {
        public Rectangle Bounds => Halted ? Rectangle.Empty : new Rectangle(_location.ToPoint(), new Point(8, 8));

        private const int ScreenPlusBoomerangWidth = 256;
        private const int ScreenPlusBoomerangHeight = 176;
        private const int SideOffset = 4;
        private const int UpLeftOffset = -8;
        private const int DownRightOffset = 16;
        private const int WrapDistanceHorizontal = 116;
        private const int WrapDistanceVertical = 76;
        private const int DistancePerFrame = 2;

        private Vector2 _location;
        private readonly ISprite _sprite = ProjectileSpriteFactory.Instance.CreateThrownATWBoomerang();

        private Vector2? _collisionLocation;
        private ISprite _collision;
        private bool _returning;

        private int _framesDelayed;
        private readonly IPlayer _player;
        private readonly Direction _direction;
        public bool Halted { get; private set; }

        private readonly SoundEffectInstance _soundEffect;

        public ATWBoomerang(IPlayer player, Direction direction)
        {
            _soundEffect = SoundEffectManager.Instance.PlayBoomerang();
            _player = player;
            _direction = direction;
            switch (_direction)
            {
                case Direction.Up:
                    _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + UpLeftOffset);
                    break;
                case Direction.Left:
                    _location = new Vector2(_player.Location.X + UpLeftOffset, _player.Location.Y + SideOffset);
                    break;
                case Direction.Right:
                    _location = new Vector2(_player.Location.X + DownRightOffset, _player.Location.Y + SideOffset);
                    break;
                case Direction.Down:
                    _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + DownRightOffset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            Halted = true;
            player.Inventory.AddSecondaryItem(Secondary.ATWBoomerang);
            _soundEffect.Stop();
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
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

        private void UpdateNonReturningLocation()
        {

        }

        public void Update()
        {
            _sprite.Update();

            if (_returning)
            {
                switch (_direction)
                {
                    case Direction.Up:
                        _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + UpLeftOffset
                            - DistancePerFrame * _framesDelayed + ScreenPlusBoomerangHeight);
                        break;
                    case Direction.Left:
                        _location = new Vector2(_player.Location.X + UpLeftOffset - DistancePerFrame * _framesDelayed
                            + ScreenPlusBoomerangWidth, _player.Location.Y + SideOffset);
                        break;
                    case Direction.Right:
                        _location = new Vector2(_player.Location.X + DownRightOffset + DistancePerFrame * _framesDelayed
                            - ScreenPlusBoomerangWidth, _player.Location.Y + SideOffset);
                        break;
                    case Direction.Down:
                        _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + DownRightOffset
                            + DistancePerFrame * _framesDelayed - ScreenPlusBoomerangHeight);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (_direction)
                {
                    case Direction.Up:
                        if (_framesDelayed * DistancePerFrame >= WrapDistanceVertical)
                            _returning = true;
                        _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + UpLeftOffset
                            - DistancePerFrame * _framesDelayed);
                        break;
                    case Direction.Left:
                        if (_framesDelayed * DistancePerFrame >= WrapDistanceHorizontal)
                            _returning = true;
                        _location = new Vector2(_player.Location.X + UpLeftOffset - DistancePerFrame * _framesDelayed,
                            _player.Location.Y + SideOffset);
                        break;
                    case Direction.Right:
                        if (_framesDelayed * DistancePerFrame >= WrapDistanceHorizontal)
                            _returning = true;
                        _location = new Vector2(_player.Location.X + DownRightOffset + DistancePerFrame * _framesDelayed,
                            _player.Location.Y + SideOffset);
                        break;
                    case Direction.Down:
                        if (_framesDelayed * DistancePerFrame >= WrapDistanceVertical)
                            _returning = true;
                        _location = new Vector2(_player.Location.X + SideOffset, _player.Location.Y + DownRightOffset
                            + DistancePerFrame * _framesDelayed);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            _framesDelayed++;
        }

        public void Halt()
        {
            // NO-OP
        }

        public void Reflect(Direction direction)
        {
            //NO-OP
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
