using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class GoriyaAgent
    {
        private Goriya _goriya;
        private ISprite _sprite;

        private bool _updateSpriteFlag;
        private StatusHealth _statusHealth;
        private StatusMoving _statusMoving;

        private Direction _statusDirection;

        private Vector2 _previousLocation;

        private int _health;
        private int _posX;
        private int _posY;

        private readonly SpriteBatch _spriteBatch;

        private enum StatusHealth
        {
            Alive, Dead, Damaged
        };

        private enum StatusMoving
        {
            Idle, Moving
        };


        public GoriyaAgent(Goriya goriya, SpriteBatch spriteBatch, int posX, int posY)
        {
            _updateSpriteFlag = false;
            _posX = posX;
            _posY = posY;
            _goriya = goriya;
            _statusHealth = StatusHealth.Dead;
            _spriteBatch = spriteBatch;
            _previousLocation = new Vector2(posX, posY);
            _statusDirection = Direction.Down;
            _health = 2;
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
        }

        public void Idle()
        {
            _statusMoving = StatusMoving.Idle;
        }

        public void Kill()
        {
            _statusHealth = StatusHealth.Dead;
        }

        public void MoveDown()
        {
            UpdateMoving(Direction.Down);
            _posY += 1;
        }

        public void UseAttack()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            UpdateMoving(Direction.Left);
            _posX -= 1;
        }

        public void MoveRight()
        {
            UpdateMoving(Direction.Right);
            _posX += 1;
        }

        public void MoveUp()
        {
            UpdateMoving(Direction.Up);
            _posY -= 1;
        }

        public void Spawn()
        {
            _statusHealth = StatusHealth.Alive;
        }

        public void TakeDamage()
        {
            if (_statusHealth == StatusHealth.Alive)
            {
                _health--;
                _statusHealth = StatusHealth.Damaged;
                _sprite.PaletteShift();
            }

            if (_health < 1)
            {
                _statusHealth = StatusHealth.Dead;
            }
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, new Vector2(_posX, _posY));
        }

        public void Update()
        {
            if (_updateSpriteFlag)
            {
                UpdateSprite();
            }
            _sprite.Update();
        }

        private void UpdateMoving(Direction direction)
        {
            _updateSpriteFlag = (_statusMoving != StatusMoving.Moving) || (_statusDirection != direction);
            _statusMoving = StatusMoving.Moving;
            _statusDirection = direction;
            
        }

        private void UpdateSprite()
        {
            
            switch(_statusDirection)
            {
                case Direction.Down:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
                    break;
                case Direction.Left:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceLeft();
                    break;
                case Direction.Up:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceUp();
                    break;
                case Direction.Right:
                    _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}