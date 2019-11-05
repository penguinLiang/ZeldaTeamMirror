using Microsoft.Xna.Framework;
using System;
using Zelda.Commands;
using Zelda.Projectiles;

namespace Zelda.Enemies
{
    public class OldMan : EnemyAgent
    {
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

        public override void TakeDamage()
        {
            Sprite.PaletteShift();
            _attacked = true;
        }

        public override void Update(Point playerLocation)
        {
            _playerLocation = playerLocation;
            Sprite.Update();
            if (_attacked)
            {
                if (_attackDelay-- == 0)
                {
                    _attackDelay = 180;
                    UseAttack();
                }
            }
        }

        private void UseAttack()
        {
            var fb0Location = new Point(Location.X - 56, Location.Y + 8);
            var fb2Location = new Point(Location.X + 56, Location.Y + 8);

            Projectiles.Add(new Fireball(fb0Location, GenerateFireballVector(fb0Location), false));
            Projectiles.Add(new Fireball(fb2Location, GenerateFireballVector(fb2Location), false));
        }

        private Vector2 GenerateFireballVector(Point fbLocation)
        {
            double xDiff = _playerLocation.X - fbLocation.X;
            double yDiff = _playerLocation.Y - fbLocation.Y;

            var magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            var normalizedY = yDiff / magnitude;
            var normalizedX = xDiff / magnitude;
            var xVelocity = 1.2f;
            var yVelocity = 2f;
            return new Vector2(xVelocity * (float)normalizedX, yVelocity * (float)normalizedY);
        }

        public override void Draw()
        {
            Sprite.Draw(Location.ToVector2());
        }
    }
}
