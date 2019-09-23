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
}
