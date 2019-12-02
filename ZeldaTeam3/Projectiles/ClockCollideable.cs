using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class ClockCollideable : IProjectile
    {
        private const int FramesToDisappear = 661;
        private readonly Point _roomSize = new Point(768, 528);

        public Rectangle Bounds => new Rectangle(Point.Zero, _roomSize);
        public bool Halted { get; private set; }

        private int _framesDelayed;

        public ClockCollideable()
        {
            SoundEffectManager.Instance.PlayKeyAppear();
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new EnemyStun(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            //NO-OP
        }

        public void Update()
        {
            if (++_framesDelayed == FramesToDisappear)
                Halted = true;
        }

        public void Draw()
        {
            //NO-OP
        }
    }
}
