using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Projectiles
{
    public class SwordBeamParticles : IProjectile
    {
        private const int NumberOfParticles = 4;
        private const int FramesToDisappear = 30;

        public Rectangle Bounds { get; } = Rectangle.Empty;
        public bool Halted { get; set; } // Halted is used to remove this instance from the list that is drawing it

        private readonly Point[] _locations = new Point[NumberOfParticles];
        private readonly ISprite[] _sprites = new ISprite[NumberOfParticles];
        private int _framesDelayed;
        
        public SwordBeamParticles(Point swordLocation)
        {
            _locations[0] = new Point(swordLocation.X - 8, swordLocation.Y - 8);
            _locations[1] = new Point(swordLocation.X + 8, swordLocation.Y - 8);
            _locations[2] = new Point(swordLocation.X - 8, swordLocation.Y + 8);
            _locations[3] = new Point(swordLocation.X + 8, swordLocation.Y + 8);
            _sprites[0] = ProjectileSpriteFactory.Instance.CreateSwordBeamParticleTopLeft();
            _sprites[1] = ProjectileSpriteFactory.Instance.CreateSwordBeamParticleTopRight();
            _sprites[2] = ProjectileSpriteFactory.Instance.CreateSwordBeamParticleBottomLeft();
            _sprites[3] = ProjectileSpriteFactory.Instance.CreateSwordBeamParticleBottomRight();
        }
        
        public bool CollidesWith(Rectangle rectangle)
        {
            return false;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
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

        public void Halt()
        {
            // NO-OP
        }

        public void Update()
        {
            _locations[0].X--;
            _locations[0].Y--;
            _locations[1].X++;
            _locations[1].Y--;
            _locations[2].X--;
            _locations[2].Y++;
            _locations[3].X++;
            _locations[3].Y++;

            for (var i = 0; i < NumberOfParticles; i++)
            {
                _sprites[i].Update();

                if (_framesDelayed != FramesToDisappear) continue;
                _sprites[i].Hide();
                Halted = true;
            }
            _framesDelayed++;
        }

        public void Draw()
        {
            for (var i = 0; i < NumberOfParticles; i++)
            {
                _sprites[i].Draw(_locations[i].ToVector2());
            }
        }
    }
}
