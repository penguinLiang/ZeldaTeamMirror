using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Player
{
    internal class PlayerSwordCollision : ICollideable
    {
        private readonly MovementStateMachine _movementStateMachine;

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

        public PlayerSwordCollision(MovementStateMachine movementStateMachine)
        {
            _movementStateMachine = movementStateMachine;
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new SpawnableDamage(enemy);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return NoOp.Instance;
        }
    }
}
