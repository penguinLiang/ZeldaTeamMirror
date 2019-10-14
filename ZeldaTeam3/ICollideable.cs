using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface ICollideable
    {
        bool CollidesWith(Rectangle rect);
        ICommand PlayerEffect(IPlayer player);
        ICommand EnemyEffect(IEnemy enemy);
        ICommand ProjectileEffect(IHaltable projectile);
    }
}
