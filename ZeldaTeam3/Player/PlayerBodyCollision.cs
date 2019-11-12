using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Projectiles;

namespace Zelda.Player
{
    internal class PlayerBodyCollision : ICollideable
    {
        private readonly IPlayer _player;

        // Link only collides with the bottom half of his sprite, hence the offset by 8 in the y and the height only being 8. This is true to the source.
        public Rectangle Bounds => new Rectangle(_player.Location.X, _player.Location.Y + 8, 16, 8);

        public PlayerBodyCollision(IPlayer player)
        {
            _player = player;
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
            if (projectile is SwordBeam) return NoOp.Instance;
            return new MoveableHalt(projectile);
        }
    }
}
