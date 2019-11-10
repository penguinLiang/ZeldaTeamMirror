using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Items;

namespace Zelda.Player
{
    internal class PlayerSwordCollision : ICollideable
    {
        private readonly MovementStateMachine _movementStateMachine;
        private readonly int _damage;

        public Rectangle Bounds
        {
            get
            {
                switch(_movementStateMachine.Facing)
                {
                    case Direction.Up:
                        return new Rectangle(_movementStateMachine.Location.X + 4, _movementStateMachine.Location.Y - 12, 8, 12);
                    case Direction.Right:
                        return new Rectangle(_movementStateMachine.Location.X + 16, _movementStateMachine.Location.Y + 4, 12, 8);
                    case Direction.Left:
                        return new Rectangle(_movementStateMachine.Location.X - 12, _movementStateMachine.Location.Y + 4, 12, 8);
                    case Direction.Down:
                        return new Rectangle(_movementStateMachine.Location.X + 4, _movementStateMachine.Location.Y + 16, 8, 12);
                    default:
                        throw new NotImplementedException("Invalid direction parameter.");
                }
            }
        }

        public PlayerSwordCollision(MovementStateMachine movementStateMachine, Primary swordLevel)
        {
            _movementStateMachine = movementStateMachine;
            switch (swordLevel)
            {
                case Primary.Sword:
                    _damage = 1;
                    break;
                case Primary.WhiteSword:
                    _damage = 2;
                    break;
                case Primary.MagicalSword:
                    _damage = 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CollidesWith(Rectangle rect)
        {
            return rect.Size != Point.Zero && Bounds.Intersects(rect);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new SpawnableDamage(enemy, _damage);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }
    }
}
