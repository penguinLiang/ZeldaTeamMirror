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
   public class ProjectileStateMachine 
    {
        private ISprite _arrowSprite;
        private ArrowAndSwordBeamStateMachine _statemachine;
        private Vector2 _location;
        private SpriteBatch _spritebatch;
        private int _direction;
        public ProjectileStateMachine(SpriteBatch spritebatch, Vector2 location, ISprite currentSprite, int direction )
        {
            _spritebatch = spritebatch;
            _location = location;
            _arrowSprite = currentSprite;
            _direction = direction;
            _statemachine = new ArrowAndSwordBeamStateMachine(_spritebatch,_location, _arrowSprite, _direction);
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

    public class ArrowAndSwordBeamStateMachine
    {
        private Vector2 _location;
        private SpriteBatch _spriteBatch;
        private ISprite _currentSprite;
        private int MaxFramesAway = 48;
        private int currentFrame = 0;
        private int DistancePerFrame = 4;

        public int _direction = 0;
        public ArrowAndSwordBeamStateMachine(SpriteBatch spriteBatch, Vector2 location, ISprite currentSprite, int direction)
        {
            currentFrame = 0;
            _direction = direction;
            _location = location;
            _currentSprite = currentSprite;
            _spriteBatch = spriteBatch;     
        }

        public void Update()
        {
            if (currentFrame < MaxFramesAway)
            {
                if (_direction == 0)
                {
                    _location.Y = _location.Y + DistancePerFrame;
                }
                else if (_direction == 1)
                {
                    _location.X = _location.X - DistancePerFrame;
                }
                else if (_direction == 2)
                {
                    _location.X = _location.X + DistancePerFrame;
                }
                else if (_direction == 3)
                {
                    _location.Y = _location.Y - DistancePerFrame;
                }
                currentFrame++;
            }
        }
        public void Draw()
        {
           _currentSprite.Draw(_spriteBatch, _location);
        }
    }
}
