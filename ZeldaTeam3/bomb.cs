using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public class Bomb
    {
        private ISprite _arrowSprite;
        private BombStateMachine _statemachine;
        private Vector2 _location;
        private SpriteBatch _spritebatch;
        public Bomb(SpriteBatch spritebatch, Vector2 location, ISprite currentSprite)
        {
            _spritebatch = spritebatch;
            _location = location;
            _arrowSprite = currentSprite;
            _statemachine = new BombStateMachine(_spritebatch, _location, _arrowSprite );

        }

        public void Update()
        {
            _statemachine.Update();
        }

        public void Draw()
        {
            _statemachine.Draw();
        }
    }

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