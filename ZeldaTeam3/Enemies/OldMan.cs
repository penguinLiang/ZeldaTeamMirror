using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Projectiles;
using Zelda.SoundEffects;

namespace Zelda.Enemies
{
    public class OldMan : EnemyAgent
    {
        private const int AttackDelay = 180;
        private static readonly Point FireballLeftOffset = new Point(-56, 8);
        private static readonly Point FireballRightOffset = new Point(56, 8);

        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateOldMan();
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        public override bool Alive => true;

        private bool _attacked;
        private int _attackDelay;
        private Point _playerLocation;

        public override void Halt()
        {
            // NO-OP: Old man doesn't move
        }

        protected override void Knockback()
        {
            // NO-OP: Immovable
        }

        public OldMan(Point location)
        {
            Location = location + new Point(8, 0);
            _attacked = false;
            _attackDelay = 0;
        }

        public override void Spawn()
        {
            _attacked = false;
            base.Spawn();
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
        }

        public override void TakeDamage(int damage)
        {
            Sprite.PaletteShift();
            _attacked = true;
            SoundEffectManager.Instance.PlayEnemyHit();
        }

        public override void Target(Point location)
        {
            _playerLocation = location;
        }

        public override void Update()
        {
            Sprite.Update();

            if (!_attacked || _attackDelay-- != 0) return;
            _attackDelay = AttackDelay;
            UseAttack();
        }

        private void UseAttack()
        {
            var fbLLocation = Location + FireballLeftOffset;
            var fbRLocation = Location + FireballRightOffset;

            Projectiles.Add(new Fireball(fbLLocation, GenerateFireballVector(fbLLocation), false));
            Projectiles.Add(new Fireball(fbRLocation, GenerateFireballVector(fbRLocation), false));
        }

        private Vector2 GenerateFireballVector(Point fbLocation)
        {
            const float xVelocity = 1.2f;
            const float yVelocity = 2f;

            double xDiff = _playerLocation.X - fbLocation.X;
            double yDiff = _playerLocation.Y - fbLocation.Y;

            var magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            var normalizedY = yDiff / magnitude;
            var normalizedX = xDiff / magnitude;
            return new Vector2(xVelocity * (float)normalizedX, yVelocity * (float)normalizedY);
        }

        public override void Draw()
        {
            Sprite.Draw(Location.ToVector2());
        }
    }
}
