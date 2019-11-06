using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    public abstract class DoorBase : ICollideable, IDrawable, IActivatable
    {
        protected abstract ISprite Sprite { get; }
        protected abstract Point Location { get; }
        protected abstract Point Size { get; }
        protected abstract Point DrawOffset { get; }

        protected abstract Rectangle NoOpArea { get; }

        protected abstract Rectangle TransitionArea { get; }
        protected abstract ICommand TransitionEffect { get; }

        protected Rectangle LocationOffset(Rectangle rect) => new Rectangle(rect.Location + Location, rect.Size);

        public Rectangle Bounds => new Rectangle(Location, Size);

        public virtual void Reset()
        {
            // NO-OP
        }

        public virtual void Unblock()
        {
            // NO-OP
        }

        public virtual void Activate()
        {
            // NO-OP
        }

        public virtual void Deactivate()
        {
            // NO-OP
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public virtual ICommand PlayerEffect(IPlayer player)
        {
            if (player.BodyCollision.CollidesWith(LocationOffset(TransitionArea)))
            {
                return TransitionEffect;
            }

            if (player.BodyCollision.CollidesWith(LocationOffset(NoOpArea)))
            {
                return NoOp.Instance;
            }
            return new MoveableHalt(player);
        }

        public virtual ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public virtual ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            Sprite?.Update();
        }

        public void Draw()
        {
            Sprite?.Draw((Location + DrawOffset).ToVector2());
        }
    }
}
