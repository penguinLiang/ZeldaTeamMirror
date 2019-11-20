using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Projectiles
{
    internal class SwordBeam : IProjectile
    {
        private const int FramesToDisappear = 140;
        private const int SwordBeamSpeed = 3;

        private readonly ISprite _sprite;
        private readonly BasicProjectileStateMachine _swordBeamStateMachine;
        private readonly int _damage;

        public Rectangle Bounds => _swordBeamStateMachine.Bounds;
        public bool Halted { get; set; }

        private int _framesDelayed;

        public SwordBeam(Point location, Direction direction, int damage)
        {
            switch (direction)
            {
                case Direction.Up:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamUp();
                    break;
                case Direction.Down:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamDown();
                    break;
                case Direction.Left:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamLeft();
                    break;
                case Direction.Right:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSwordBeamRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _swordBeamStateMachine = new BasicProjectileStateMachine(location, direction, SwordBeamSpeed);
            _damage = damage;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _swordBeamStateMachine.CollidesWith(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            _sprite.Hide();
            _swordBeamStateMachine.ClearBounds();
            return new SpawnableDamage(enemy, _damage);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            Halted = true;
        }

        public void Reflect(Direction direction)
        {
            //NO-OP
        }

        public void Update()
        {
            _swordBeamStateMachine.Update();
            if (_framesDelayed++ == FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_swordBeamStateMachine.Location.ToVector2());
        }
    }
}
