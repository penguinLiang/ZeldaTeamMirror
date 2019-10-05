using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Projectiles
{
    public class Bomb : IDrawable
    {
        private const int FramesToExplosion = 100;
        private const int FramesToDisappear = 160;

        private readonly Vector2 _location;
        private readonly SpriteBatch _spriteBatch;
        private ISprite _sprite;
        private BombExplosion _explosion;
        private int _framesDelayed;

        public Bomb(SpriteBatch spriteBatch, Vector2 location)
        {
            _location = location;
            _sprite = Items.ItemSpriteFactory.Instance.CreateBomb();
            _spriteBatch = spriteBatch;
        }

        public void Update()
        {
            _sprite.Update();
            
            if (++_framesDelayed == FramesToExplosion)
            {
                _explosion = new BombExplosion(_spriteBatch, new Vector2(_location.X - 16, _location.Y - 16));
                _sprite.Hide();
            }
            if (_framesDelayed >= FramesToExplosion)
            {
                _explosion.Update();
            }
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, _location);
            if (_framesDelayed >= FramesToExplosion)
            {
                _explosion.Draw();
            }
        }
    }
}
