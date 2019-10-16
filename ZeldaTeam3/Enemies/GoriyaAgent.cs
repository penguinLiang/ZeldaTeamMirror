using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class GoriyaAgent
    {
        private ISprite _sprite;
        
        private const int BoomerangDuration = 40;
        private Projectiles.GoriyaBoomerang _boomerang;

        private bool _alive;
        private bool _isDying;
        private bool _isImmobile;
        private bool _updateSpriteFlag;

        private Point _location;

        private Direction _statusDirection;

        private int _health;
        private int _clock;
        private int _timeSinceBoomerangThrown;

        private enum StatusHealth
        {
            Alive,
            Dead
        }

        public GoriyaAgent(Point location)
        {
            _updateSpriteFlag = false;
            _location = location;
            _timeSinceBoomerangThrown = BoomerangDuration;
            _alive = false;
            _statusDirection = Direction.Down;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
            _sprite.Hide();
        }

        public void Kill()
        {
            if (!_alive)
            {
                return;
            }
            _sprite.Hide();
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            _clock = 32;
            _isDying = true;
            _alive = false;
        }

        public void UseAttack()
        {
            var boomerangeOffset = new Point(4, 4);
            switch (_statusDirection)
            {
                case Direction.Up:
                    boomerangeOffset.Y -= 16;
                    break;
                case Direction.Down:
                    boomerangeOffset.Y += 16;
                    break;
                case Direction.Left:
                    boomerangeOffset.X -= 16;
                    break;
                case Direction.Right:
                    boomerangeOffset.X += 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _boomerang = new Projectiles.GoriyaBoomerang(_location + boomerangeOffset, _statusDirection);
            _timeSinceBoomerangThrown = 0;
        }

        public void MoveLeft()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Left);
                _location.X -= 1;
            }
        }

        public void MoveRight()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Right);
                _location.X += 1;
            }
        }

        public void MoveUp()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Up);
                _location.Y -= 1;
            }
        }

        public void MoveDown()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Down);
                _location.Y += 1;
            }
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            _alive = true;
            _clock = 30;
            _health = 10;
            UpdateDirection(Direction.Down);
        }

        public void TakeDamage()
        {
            if (_alive)
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
            _sprite.Draw(_location.ToVector2());
            _boomerang?.Draw();
        }

        public void Update()
        {
            _timeSinceBoomerangThrown++;

            if (_clock > 0)
            {
                _clock--;
                if (_clock == 0)
                {
                    CheckFlags();
                }
            }
            
            if (_updateSpriteFlag)
            {
                UpdateSprite();
            }

            _sprite.Update();
            _boomerang?.Update();
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

            if (!_alive)
            {
                _sprite.Hide();
            }

            _updateSpriteFlag = false;
        }

        private void CheckFlags()
        {
            if (_isImmobile)
            {
                _updateSpriteFlag = true;
                _isImmobile = false;
            }

            if (_isDying)
            {
                _sprite = EnemySpriteFactory.Instance.CreateStalfos();
                _sprite.Hide();
                _isDying = false;
            }
        }
    }
}
