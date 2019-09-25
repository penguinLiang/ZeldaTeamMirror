using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Projectiles
{

    public class BombStateMachine
    {
        private Vector2 _location;
        private SpriteBatch _spriteBatch;
        private ISprite _currentSprite;
        private int TimeToExplosion = 100;
        private int TimeElapsed = 0;

        public int _direction;
        public BombStateMachine(SpriteBatch spriteBatch, Vector2 location, ISprite currentSprite)
        {
            _location = location;
            _currentSprite = currentSprite;
            _spriteBatch = spriteBatch;

        }

        public void Update()
        {
            TimeElapsed++;

            if (TimeElapsed >= TimeToExplosion)
            {
                _currentSprite = ProjectileSpriteFactory.Instance.CreateBombExplosion();
            }
        }


        public void Draw()
        {
            _currentSprite.Draw(_spriteBatch, _location);

        }
    }
}
