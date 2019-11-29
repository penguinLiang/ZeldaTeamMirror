using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Projectiles;

namespace Zelda.Player
{
    internal class PlayerBodyCollision : ICollideable
    {
        private readonly IPlayer _player;
        private readonly bool _partyTime;

        // Link only collides with the bottom half of his sprite, hence the offset by 8 in the y and the height only being 8. This is true to the source.
        public Rectangle Bounds => new Rectangle(_player.Location.X, _player.Location.Y + 8, 16, 8);

        public PlayerBodyCollision(IPlayer player, bool partyTime)
        {
            _player = player;
            _partyTime = partyTime;
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return _partyTime ? (ICommand)new SpawnableDamage(enemy, 100) : NoOp.Instance;
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
