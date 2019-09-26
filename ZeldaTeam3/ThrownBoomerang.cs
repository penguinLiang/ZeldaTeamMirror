﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
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

    public class ThrownBoomerangStateMachine
    {
        private Vector2 _location;
        private SpriteBatch _spriteBatch;
        private ISprite _currentSprite;
        private int MaxDistance = 100;
        private int currentDistanceAway;
        private int DistancePerFrame = 5;

        public int _direction;
        public ThrownBoomerangStateMachine(SpriteBatch spriteBatch, Vector2 location, ISprite currentSprite, int direction)
        {
            _direction = direction;
            _location = location;
            _currentSprite = currentSprite;
            _spriteBatch = spriteBatch;

            currentDistanceAway = 0;


        }

        public void Update()
        {
          
            if (currentDistanceAway >= MaxDistance)
            {
                _direction = _direction + 1;
                currentDistanceAway = 0;
            }

            switch (_direction)
                {
                    case 0:
                        _location.Y = _location.Y + DistancePerFrame;
                        break;
                case 1:
                    _location.Y = _location.Y - DistancePerFrame;
                    break;
                case 2:
                        _location.X = _location.X - DistancePerFrame;
                        break;
                    case 3:
                        _location.X = _location.X + DistancePerFrame;
                        break;
                   
                }
                currentDistanceAway= currentDistanceAway+DistancePerFrame;

        }

        public void Draw()
        {
            _currentSprite.Draw(_spriteBatch, _location);

        }
    }
}