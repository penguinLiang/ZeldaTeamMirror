using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public virtual bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public virtual ICommand PlayerEffect(IPlayer player)
        {
            return new SpawnableDamage(player);
        }

        public virtual ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public virtual ICommand ProjectileEffect(IHaltable projectile)
        {
            return new MoveableHalt(projectile);
        }

        public virtual void Knockback()
        {
            throw new NotImplementedException();
        }

        public virtual void Halt()
        {
            throw new NotImplementedException();
        }

        public virtual void Stun()
        {
            throw new NotImplementedException();
        }

        public abstract bool Alive { get; }
        public abstract Rectangle Bounds { get; }
        public abstract void Draw();
        public abstract void Spawn();
        public abstract void TakeDamage();
        public abstract void Update();
    }
}
