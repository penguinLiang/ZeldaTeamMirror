using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class LaserBeam : IProjectile
    {
        private const int FramesToDisappear = 120;
        private const int LaserBeamSpeed = 4;

        private readonly Direction _direction;
        private readonly ISprite _startSprite;
        private readonly ISprite _middleSprite;
        private readonly ISprite _endSprite;

        public Rectangle Bounds { get; private set; }
        public bool Halted { get; set; }

        private Point _startLocation;
        private Point _endLocation;
        private int _framesDelayed;

        public LaserBeam(Point location, Direction direction)
        {
            _direction = direction;
            switch (direction)
            {
                case Direction.Up:
                    _startLocation = new Point(location.X - 16, location.Y);
                    _endLocation = new Point(_startLocation.X, _startLocation.Y - 24);
                    _startSprite = ProjectileSpriteFactory.Instance.CreateLaserStartUp();
                    _middleSprite = ProjectileSpriteFactory.Instance.CreateLaserMiddleVertical();
                    _endSprite = ProjectileSpriteFactory.Instance.CreateLaserEndUp();
                    Bounds = new Rectangle(_endLocation.X + 10, _endLocation.Y + 10, 28, 30);
                    break;
                case Direction.Down:
                    _startLocation = new Point(location.X - 16, location.Y);
                    _endLocation = new Point(_startLocation.X, _startLocation.Y + 8);
                    _startSprite = ProjectileSpriteFactory.Instance.CreateLaserStartDown();
                    _middleSprite = ProjectileSpriteFactory.Instance.CreateLaserMiddleVertical();
                    _endSprite = ProjectileSpriteFactory.Instance.CreateLaserEndDown();
                    Bounds = new Rectangle(_startLocation.X + 10, _startLocation.Y, 28, 30);
                    break;
                case Direction.Left:
                    _startLocation = new Point(location.X, location.Y - 16);
                    _endLocation = new Point(_startLocation.X - 24, _startLocation.Y);
                    _startSprite = ProjectileSpriteFactory.Instance.CreateLaserStartLeft();
                    _middleSprite = ProjectileSpriteFactory.Instance.CreateLaserMiddleHorizontal();
                    _endSprite = ProjectileSpriteFactory.Instance.CreateLaserEndLeft();
                    Bounds = new Rectangle(_endLocation.X + 10, _endLocation.Y + 10, 30, 28);
                    break;
                case Direction.Right:
                    _startLocation = new Point(location.X, location.Y - 16);
                    _endLocation = new Point(_startLocation.X + 8, _startLocation.Y);
                    _startSprite = ProjectileSpriteFactory.Instance.CreateLaserStartRight();
                    _middleSprite = ProjectileSpriteFactory.Instance.CreateLaserMiddleHorizontal();
                    _endSprite = ProjectileSpriteFactory.Instance.CreateLaserEndRight();
                    Bounds = new Rectangle(_startLocation.X, _startLocation.Y + 10, 30, 28);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SoundEffectManager.Instance.PlayLaserBeam();
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new SpawnableDamage(enemy, 5);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            //NO-OP
        }

        public void Update()
        {
            if (_framesDelayed++ == FramesToDisappear)
            {
                _startSprite.Hide();
                _middleSprite.Hide();
                _endSprite.Hide();
                Halted = true;
            }
            _startSprite.Update();
            _middleSprite.Update();
            _endSprite.Update();

            switch (_direction)
            {
                case Direction.Up when Bounds.Height <= 80:
                    Bounds = new Rectangle(Bounds.X, Bounds.Y - LaserBeamSpeed, Bounds.Width, Bounds.Height + LaserBeamSpeed);
                    _endLocation.Y -= LaserBeamSpeed;
                    break;
                case Direction.Down when Bounds.Height <= 80:
                    Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height + LaserBeamSpeed);
                    _endLocation.Y += LaserBeamSpeed;
                    break;
                case Direction.Left when Bounds.Width <= 120:
                    Bounds = new Rectangle(Bounds.X - LaserBeamSpeed, Bounds.Y, Bounds.Width + LaserBeamSpeed, Bounds.Height);
                    _endLocation.X -= LaserBeamSpeed;
                    break;
                case Direction.Right when Bounds.Width <= 120:
                    Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width + LaserBeamSpeed, Bounds.Height);
                    _endLocation.X += LaserBeamSpeed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Draw()
        {
            _startSprite.Draw(_startLocation.ToVector2());
            _endSprite.Draw(_endLocation.ToVector2());

            switch (_direction)
            {
                case Direction.Up:
                    for (var offset = 24; offset < _startLocation.Y - _endLocation.Y; offset += LaserBeamSpeed)
                    {
                        _middleSprite.Draw(new Vector2(_endLocation.X, _endLocation.Y + offset));
                    }
                    break;
                case Direction.Down:
                    for (var offset = 16; offset < _endLocation.Y - _startLocation.Y + 8; offset += LaserBeamSpeed)
                    {
                        _middleSprite.Draw(new Vector2(_startLocation.X, _startLocation.Y + offset));
                    }
                    break;
                case Direction.Left:
                    for (var offset = 24; offset < _startLocation.X - _endLocation.X; offset += LaserBeamSpeed)
                    {
                        _middleSprite.Draw(new Vector2(_endLocation.X + offset, _endLocation.Y));
                    }
                    break;
                case Direction.Right:
                    for (var offset = 16; offset < _endLocation.X - _startLocation.X + 8; offset += LaserBeamSpeed)
                    {
                        _middleSprite.Draw(new Vector2(_startLocation.X + offset, _startLocation.Y));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
