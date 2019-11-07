using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, ICollideable, IDrawable
    {
        void Target(Point location);
        List<IProjectile> Projectiles { get; set; }
    }
}
