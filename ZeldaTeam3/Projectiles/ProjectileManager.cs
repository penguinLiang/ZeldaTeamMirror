using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Projectiles
{
    class ProjectileManager
    {
        public List<IProjectile> Projectiles = new List<IProjectile>();

        public void CheckProjectiles()
        {
//Are any projectiles expired?
//Did any collide with a wall?
        }

        public void RemoveProjectiles(int i)
        {
            //remove the specified projectile at i location
        }


    }
}
