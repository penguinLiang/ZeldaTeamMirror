using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Zelda.Projectiles
{
    public class BombExplosion : IDrawable
    {
        private const int FramesToDisappear = 60;
        private const int NumberOfSprites = 4;

        private readonly Vector2 _location;
        private readonly SpriteBatch _spriteBatch;
        private Vector2[] _spriteLocations = new Vector2[NumberOfSprites];
        private ISprite[] _sprites = new ISprite[NumberOfSprites];
        private int _framesDelayed;

        private void SetSmokeCloudLocations()
        {
            Random rand = new Random();

            _spriteLocations[0] = new Vector2(_location.X + 16, _location.Y + 16);
            if (rand.Next(2) == 0)
            {
                _spriteLocations[1] = new Vector2(_location.X + 8, _location.Y);
                _spriteLocations[2] = new Vector2(_location.X + 32, _location.Y + 16);
                _spriteLocations[3] = new Vector2(_location.X + 24, _location.Y + 32);
            }
            else
            {
                _spriteLocations[1] = new Vector2(_location.X + 24, _location.Y);
                _spriteLocations[2] = new Vector2(_location.X, _location.Y + 16);
                _spriteLocations[3] = new Vector2(_location.X + 8, _location.Y + 32);
            }
        }

        public BombExplosion(SpriteBatch spriteBatch, Vector2 location)
        {
            _location = location;
            _spriteBatch = spriteBatch;
            SetSmokeCloudLocations();
            for (int i = 0; i < NumberOfSprites; i++)
            {
                _sprites[i] = ProjectileSpriteFactory.Instance.CreateBombExplosion();
            }
        }

        public void Update()
        {
            if (_framesDelayed++ == FramesToDisappear)
            {
                foreach (var sprite in _sprites)
                {
                    sprite.Hide();
                }
            }
            foreach (var sprite in _sprites)
            {
                sprite.Update();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < NumberOfSprites; i++)
            {
                _sprites[i].Draw(_spriteBatch, _spriteLocations[i]);
            }
        }
    }
}
