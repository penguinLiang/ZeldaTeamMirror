using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    public class SwordBeamParticles : IDrawable
    {
        private const int numberOfParticles = 4;
        private const int FramesToDisappear = 30;

        //public bool Visible { get; private set; } = true;

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
                    //Visible = false;
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
