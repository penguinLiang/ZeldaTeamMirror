using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Projectiles
{
    public class Bomb : IDrawable
    {
        private const int FramesToExplosion = 100;
        private const int FramesToDisappear = 140;

        private readonly Vector2 _location;
        private readonly SpriteBatch _spriteBatch;
        private ISprite _sprite;
        private int _framesDelayed;

        public Bomb(SpriteBatch spriteBatch, Vector2 location)
        {
            _location = location;
            _sprite = ProjectileSpriteFactory.Instance.CreateBomb();
            _spriteBatch = spriteBatch;
        }

        public void Update()
        {
            _sprite.Update();
            if (++_framesDelayed == FramesToExplosion)
            {
                _sprite = ProjectileSpriteFactory.Instance.CreateBombExplosion();
            } else if (_framesDelayed == FramesToDisappear)
            {
                _sprite.Hide();
            }
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, _location);
        }
    }
}
