using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Zelda.Projectiles
{
 public  class ProjectileManager: IProjectile
    {

        public List<IProjectile> Projectiles { get; set; }
        public Rectangle Bounds { get; }

        public ProjectileManager()
        {
            Projectiles = new List<IProjectile>();
        }

        public void AddProjectile(IProjectile projectile)
        {
            Projectiles.Add(projectile);
        }

        public void RemoveProjectile(IProjectile projectile)
        {
            Projectiles.Remove(projectile);
        }
        

        public bool CollidesWith(Rectangle rect)
        {
            return false;
            //the projectile manager itself should never actually collide with things
        }

        public ICommand PlayerEffect(IPlayer player) {
            return Commands.NoOp.Instance;
        }
        
        public ICommand EnemyEffect(IEnemy enemy)
        {
            return Commands.NoOp.Instance;

        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return Commands.NoOp.Instance;

        }

        public void Halt()
        {
            //NOOP
        }

        public void Knockback()
        {
             //NOOP
        }

    }
}
