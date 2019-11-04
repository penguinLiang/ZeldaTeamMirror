using System.Collections.Generic;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, IDrawable, ICollideable
    {
        List<IProjectile> Projectiles { get; set; }
    }
}
//each enemy has an array of projectiles
//either empty or has something in it
