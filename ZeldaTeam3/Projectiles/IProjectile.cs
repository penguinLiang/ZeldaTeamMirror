using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Zelda.Projectiles
{
    public interface IProjectile:  ICollideable, IDrawable, IHaltable
    {
        List<IProjectile> Projectiles { get; set; }
        void AddProjectile();
        void removeProjectile();

    }
}

//need to be able to get the projectiles

//list the projectiles by type?

// scene should use this
//secondary item agent should use
//enemy should use

//Halt destroys projectile
//