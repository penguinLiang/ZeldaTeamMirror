using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class Bait : IProjectile
    {
        private const int FramesToDisappear = 3600;

        private readonly Vector2 _location;
        private readonly ISprite _sprite = Items.ItemSpriteFactory.Instance.CreateBait();
        private int _framesDelayed;
        public Rectangle Bounds { get; private set; }
        public bool Halted { get; set; }

        private int _health = 8;

        public Bait(Point location)
        {
            _location = location.ToVector2();
            Bounds = new Rectangle((int)_location.X, (int)_location.Y, 8, 16);
            SoundEffectManager.Instance.PlayBombDrop();
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
            _health--;
            return new SpawnableDamage(enemy, 0);
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
            _framesDelayed++;
            if (_health <= 0 || _framesDelayed == FramesToDisappear)
            {
                Halted = true;
                _sprite.Hide();
            }
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
