using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class GoriyaAgent
    {
        private ISprite _sprite;

        private const int BoomerangDuration = 40;

        private bool _updateSpriteFlag;
        private StatusHealth _statusHealth;

        private Direction _statusDirection;

        private int _health;
        private int _posX;
        private int _posY;
        private int _timeSinceBoomerangThrown;
        private Projectiles.ThrownBoomerang _boomerang;

        private readonly SpriteBatch _spriteBatch;

        private enum StatusHealth
        {
            Alive,
            Dead
        }

        public GoriyaAgent(SpriteBatch spriteBatch, int posX, int posY)
        {
            _updateSpriteFlag = false;
            _posX = posX;
            _posY = posY;
            _timeSinceBoomerangThrown = BoomerangDuration;
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

        public void UseAttack()
        {
            Vector2 boomerangLocation = new Vector2(_posX + 4, _posY + 4);
            switch (_statusDirection)
            {
                case Direction.Up:
                    boomerangLocation.Y -= 16;
                    break;
                case Direction.Down:
                    boomerangLocation.Y += 16;
                    break;
                case Direction.Left:
                    boomerangLocation.X -= 16;
                    break;
                case Direction.Right:
                    boomerangLocation.X += 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _boomerang = new Projectiles.ThrownBoomerang(_spriteBatch, boomerangLocation, _statusDirection);
            _timeSinceBoomerangThrown = 0;
        }

        public void MoveLeft()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration)
            {
                UpdateDirection(Direction.Left);
                _posX -= 1;
            }
        }

        public void MoveRight()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration)
            {
                UpdateDirection(Direction.Right);
                _posX += 1;
            }
        }

        public void MoveUp()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration)
            {
                UpdateDirection(Direction.Up);
                _posY -= 1;
            }
        }

        public void MoveDown()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration)
            {
                UpdateDirection(Direction.Down);
                _posY += 1;
            }
        }

        public void Spawn()
        {
            _sprite.Show();
            _statusHealth = StatusHealth.Alive;
            UpdateDirection(Direction.Down);
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
                Kill();
            }
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, new Vector2(_posX, _posY));
            if (_boomerang != null)
            {
                _boomerang.Draw();
            }
        }

        public void Update()
        {
            _timeSinceBoomerangThrown++;

            if (_updateSpriteFlag)
            {
                UpdateSprite();
            }

            _sprite.Update();
            if (_boomerang != null)
            {
                _boomerang.Update();
            }
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
