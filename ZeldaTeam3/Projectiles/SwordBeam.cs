using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    internal class SwordBeam : IProjectile
    {
        private const int FramesToDisappear = 140;
        private const int SwordBeamSpeed = 3;

        private readonly ISprite _sprite;
        private readonly ArrowAndSwordBeamStateMachine _swordBeamStateMachine;

        public Rectangle Bounds => _swordBeamStateMachine.Bounds;
        public bool Halted { get; set; }

        private int _framesDelayed;
        private int _damage;

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
            _swordBeamStateMachine = new ArrowAndSwordBeamStateMachine(location, direction, SwordBeamSpeed);
            _damage = damage;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _swordBeamStateMachine.CollidesWith(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            _sprite.Hide();
            _swordBeamStateMachine.ClearBounds();
            return new Commands.SpawnableDamage(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return Commands.NoOp.Instance;
        }

        public void Knockback()
        {
            // NO-OP
        }

        public void Halt()
        {
            Halted = true;
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
