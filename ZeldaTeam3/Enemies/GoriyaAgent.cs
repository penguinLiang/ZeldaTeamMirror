using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    internal class GoriyaAgent
    {
        private ISprite _sprite;

        internal Projectiles.GoriyaBoomerang Boomerang;

        public bool Alive { get; private set; }
        private bool _isDying;
        private bool _isImmobile;
        private bool _updateSpriteFlag;

        public Point Location;

        private Direction _statusDirection;

        private int _health;
        private int _clock;
        private int _timeSinceBoomerangThrown;
        private const int BoomerangDuration = 40;

        public GoriyaAgent(Point location)
        {
            _updateSpriteFlag = false;
            Location = location;
            _timeSinceBoomerangThrown = BoomerangDuration;
            Alive = false;
            _statusDirection = Direction.Down;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
            _sprite.Hide();
        }

        public void Kill()
        {
            if (!Alive)
            {
                return;
            }
            _sprite.Hide();
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            _clock = 32;
            _isDying = true;
            Alive = false;
        }

        public void UseAttack()
        {
            var boomerangOffset = new Point(4, 4);
            switch (_statusDirection)
            {
                case Direction.Up:
                    boomerangOffset.Y -= 16;
                    break;
                case Direction.Down:
                    boomerangOffset.Y += 16;
                    break;
                case Direction.Left:
                    boomerangOffset.X -= 16;
                    break;
                case Direction.Right:
                    boomerangOffset.X += 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Boomerang = new Projectiles.GoriyaBoomerang(Location + boomerangOffset, _statusDirection);
            _timeSinceBoomerangThrown = 0;
        }

        public void MoveLeft()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Left);
                Location.X -= 1;
            }
        }

        public void MoveRight()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Right);
                Location.X += 1;
            }
        }

        public void MoveUp()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Up);
                Location.Y -= 1;
            }
        }

        public void MoveDown()
        {
            if (_timeSinceBoomerangThrown > BoomerangDuration && !_isImmobile)
            {
                UpdateDirection(Direction.Down);
                Location.Y += 1;
            }
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            Alive = true;
            _clock = 30;
            _health = 3;
            UpdateDirection(Direction.Down);
        }

        public void TakeDamage()
        {
            if (Alive)
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
            _sprite.Draw(Location.ToVector2());
            Boomerang?.Draw();
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
            Boomerang?.Update();
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

            if (!Alive)
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
