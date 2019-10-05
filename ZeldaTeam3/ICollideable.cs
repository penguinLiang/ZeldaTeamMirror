using Microsoft.Xna.Framework;

namespace Zelda
{
    interface ICollideable
    {
        bool CollidesWith(Rectangle rect);
        ICommand PlayerEffect(IPlayer player);
        ICommand EnemyEffect(IEnemy enemy);
        ICommand ProjectileEffect();
    }
}
