using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Items;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class Bait : IProjectile
    {
        private const int FramesToDisappear = 900;
        private const int StartingHealth = 8;
        private const int InvincibilityTime = 30;

        private readonly Vector2 _location;
        private ISprite _sprite = ItemSpriteFactory.Instance.CreateBait(StartingHealth);
        private int _framesDelayed;
        public Rectangle Bounds { get; }
        public bool Halted { get; set; }

        private int _health = StartingHealth;
        private int _invincibilityTimer = InvincibilityTime;

        public Bait(Point location)
        {
            _location = location.ToVector2();
            Bounds = new Rectangle(location.X, location.Y, 8, 16);
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
            if (_invincibilityTimer < InvincibilityTime) return new SpawnableDamage(enemy, 0);

            _health--;
            _invincibilityTimer = 0;
            _sprite = ItemSpriteFactory.Instance.CreateBait(_health);
            SoundEffectManager.Instance.PlayBombDrop();
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
            _invincibilityTimer++;

            if (_health > 0 && _framesDelayed != FramesToDisappear) return;
            Halted = true;
            _sprite.Hide();
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
