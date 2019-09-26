using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Projectiles;

namespace Zelda.Enemies
{
    public class GoriyaAgent
    {
        private ISprite _sprite;

        private bool _updateSpriteFlag;
        private StatusHealth _statusHealth;

        private Direction _statusDirection;

        private int _health;
        private int _posX;
        private int _posY;

        private readonly SpriteBatch _spriteBatch;

        private enum StatusHealth
        {
            Alive,
            Dead
        };


        public GoriyaAgent(SpriteBatch spriteBatch, int posX, int posY)
        {
            _updateSpriteFlag = false;
            _posX = posX;
            _posY = posY;
            _statusHealth = StatusHealth.Alive;
            _spriteBatch = spriteBatch;
            _statusDirection = Direction.Down;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
            _statusHealth = StatusHealth.Dead;
        }

        public void MoveDown()
        {
            UpdateDirection(Direction.Down);
            _posY += 1;
        }

        public void UseAttack()
        {
            return;
        }

        public void MoveLeft()
        {
            UpdateDirection(Direction.Left);
            _posX -= 1;
        }

        public void MoveRight()
        {
            UpdateDirection(Direction.Right);
            _posX += 1;
        }

        public void MoveUp()
        {
            UpdateDirection(Direction.Up);
            _posY -= 1;
        }

        public void Spawn()
        {
            _sprite.Show();
            _statusHealth = StatusHealth.Alive;
            _health = 10;
        }

        public void TakeDamage()
        {
            if (_statusHealth == StatusHealth.Alive)
            {
                _health--;
                _sprite.PaletteShift();
            }

            if (_health < 1)
            {
                this.Kill();
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

        public void UpdateDirection(Direction direction)
        {
            _updateSpriteFlag = _statusDirection != direction;
            _statusDirection = direction;
        }

        private void UpdateSprite()
        {

            switch (_statusDirection)
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

            if (_statusHealth == StatusHealth.Dead)
            {
                _sprite.Hide();
            }
        }
    }
}