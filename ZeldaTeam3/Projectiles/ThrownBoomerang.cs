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
    public class ThrownBoomerang
    {
        private ISprite _arrowSprite;
        private ThrownBoomerangStateMachine _statemachine;
        private Vector2 _location;
        private SpriteBatch _spritebatch;
        private int _direction;
        public ThrownBoomerang(SpriteBatch spritebatch, Vector2 location, ISprite currentSprite, int direction)
        {
            _spritebatch = spritebatch;
            _location = location;
            _arrowSprite = currentSprite;
            _direction = direction;
            _statemachine = new ThrownBoomerangStateMachine(_spritebatch, _location, _arrowSprite, _direction);
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