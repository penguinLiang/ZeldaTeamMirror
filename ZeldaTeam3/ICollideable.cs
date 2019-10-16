using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface ICollideable
    {
        Rectangle Bounds { get; }

        bool CollidesWith(Rectangle rect);
        ICommand PlayerEffect(IPlayer player);
        ICommand EnemyEffect(IEnemy enemy);
        ICommand ProjectileEffect(IHaltable projectile);
    }
}
