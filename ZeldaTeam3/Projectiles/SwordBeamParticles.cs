using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    public class SwordBeamParticles : IProjectile
    {
        private const int numberOfParticles = 4;
        private const int FramesToDisappear = 30;

        public Rectangle Bounds { get; } = Rectangle.Empty;
        public bool Halted { get; set; } // Halted is used to remove this instance from the list that is drawing it

        private Point[] _locations = new Point[numberOfParticles];
        private ISprite[] _sprites = new ISprite[numberOfParticles];
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
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return Commands.NoOp.Instance;
        }

        public void Knockback()
        {
            // NO-OP
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

            for (int i = 0; i < numberOfParticles; i++)
            {
                _sprites[i].Update();
                if (_framesDelayed == FramesToDisappear)
                {
                    _sprites[i].Hide();
                    Halted = true;
                }
                
            }
            _framesDelayed++;
        }

        public void Draw()
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                _sprites[i].Draw(_locations[i].ToVector2());
            }
        }
    }
}
