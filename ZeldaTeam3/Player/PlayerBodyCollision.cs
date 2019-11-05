using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Player
{
    internal class PlayerBodyCollision : ICollideable
    {
        private readonly MovementStateMachine _movementStateMachine;

        // Link only collides with the bottom half of his sprite, hence the offset by 8 in the y and the height only being 8. This is true to the source.
        public Rectangle Bounds => new Rectangle(_movementStateMachine.Location.X, _movementStateMachine.Location.Y + 8, 16, 8);

        public PlayerBodyCollision(MovementStateMachine movementStateMachine)
        {
            _movementStateMachine = movementStateMachine;
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }
    }
}
