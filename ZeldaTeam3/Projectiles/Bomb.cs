using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    public class Bomb : IDrawable
    {
        private const int FramesToExplosion = 100;
        private const int FramesToDisappear = 160;
        private const int NumberOfOuterExplosionSprites = 3;

        private readonly Vector2 _location;
        private readonly Vector2[] _outerExplosionSpriteLocations = new Vector2[NumberOfOuterExplosionSprites];
        private ISprite _sprite;
        private int _framesDelayed;

        public Bomb(Point location)
        {
            _location = location.ToVector2();
            _sprite = ProjectileSpriteFactory.Instance.CreateBomb();
        }

        private void SetExplosionSpriteLocations()
        {
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                _outerExplosionSpriteLocations[0] = new Vector2(_location.X - 8, _location.Y - 16);
                _outerExplosionSpriteLocations[1] = new Vector2(_location.X + 16, _location.Y);
                _outerExplosionSpriteLocations[2] = new Vector2(_location.X + 8, _location.Y + 16);
            }
            else
            {
                _outerExplosionSpriteLocations[0] = new Vector2(_location.X + 8, _location.Y - 16);
                _outerExplosionSpriteLocations[1] = new Vector2(_location.X - 16, _location.Y);
                _outerExplosionSpriteLocations[2] = new Vector2(_location.X - 8, _location.Y + 16);
            }
        }

        public void Update()
        {
            _sprite.Update();
            
            if (++_framesDelayed == FramesToExplosion)
            {
                _sprite = ProjectileSpriteFactory.Instance.CreateBombExplosion();
                SetExplosionSpriteLocations();
            }
            else if (_framesDelayed == FramesToDisappear)
            {
                _sprite.Hide();
            }
        }

        public void Draw()
        {
            _sprite.Draw(_location);

            if (_framesDelayed < FramesToExplosion) return;
            foreach (var spriteLocation in _outerExplosionSpriteLocations)
            {
                _sprite.Draw( spriteLocation);
            }
        }
    }
}
