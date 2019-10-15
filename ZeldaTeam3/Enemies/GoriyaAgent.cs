using System;
using Microsoft.Xna.Framework;

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
        private Point _location;
        private int _timeSinceBoomerangThrown;
        private Projectiles.GoriyaBoomerang _boomerang;

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
            _statusHealth = StatusHealth.Alive;
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
            if (_timeSinceBoomerangThrown <= BoomerangDuration) return;

            UpdateDirection(Direction.Left);
            _location.X -= 1;
        }

        public void MoveRight()
        {
            if (_timeSinceBoomerangThrown <= BoomerangDuration) return;

            UpdateDirection(Direction.Right);
            _location.X += 1;
        }

        public void MoveUp()
        {
            if (_timeSinceBoomerangThrown <= BoomerangDuration) return;

            UpdateDirection(Direction.Up);
            _location.Y -= 1;
        }

        public void MoveDown()
        {
            if (_timeSinceBoomerangThrown <= BoomerangDuration) return;

            UpdateDirection(Direction.Down);
            _location.Y += 1;
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
            _sprite.Draw(_location.ToVector2());
            _boomerang?.Draw();
        }

        public void Update()
        {
            _timeSinceBoomerangThrown++;

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

            if (_statusHealth == StatusHealth.Dead)
            {
                _sprite.Hide();
            }
        }
    }
}
