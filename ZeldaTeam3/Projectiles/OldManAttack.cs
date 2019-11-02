using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Zelda.Projectiles
{
    public class OldManAttack : IProjectile
    {
        public List<IProjectile> Projectiles { get; set; }
        public void AddProjectile() { }

        public void removeProjectile() { }

        public Rectangle Bounds { get; }

        public bool CollidesWith(Rectangle rect)
        {
            new NotImplementedException();
            return false;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            new NotImplementedException();
            return null;
        }
        public ICommand EnemyEffect(IEnemy enemy)
        {
            new NotImplementedException();
            return null;
        }
        public ICommand ProjectileEffect(IHaltable projectile)
        {
            new NotImplementedException();
            return null;
        }


        public void Draw()
        {
            new NotImplementedException();

        }

        public void Update()
        {
            new NotImplementedException();

        }

        public void Knockback()
        {

        }

        public void Halt()
        {

        }

    }
}
