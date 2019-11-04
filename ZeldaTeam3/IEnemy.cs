using System.Collections.Generic;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, IDrawable, ICollideable
    {
        List<IProjectile> Projectiles { get; set; }
    }
}
