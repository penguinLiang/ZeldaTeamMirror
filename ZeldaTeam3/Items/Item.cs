using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal abstract class Item : IItem
    {
        protected Item(Point location)
        {
            Location = location;
        }

        protected abstract ISprite Sprite { get; }
        protected Point Location { get; } 

        protected virtual Point Size { get; } = new Point(8, 16);
        protected virtual Point Offset { get; } = new Point(8, 0);
        protected virtual Point DrawOffset { get; } = new Point(4, 0);

        protected bool Used { get; set; }

        public virtual Rectangle Bounds => Used ? Rectangle.Empty : new Rectangle(Location + Offset, Size);

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public virtual ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public virtual void Update()
        {
            Sprite?.Update();
        }

        public virtual void Draw()
        {
            if (!Used) Sprite?.Draw((Location + DrawOffset).ToVector2());
        }

        public virtual void Reset()
        {
            Used = false;
        }
    }
}
