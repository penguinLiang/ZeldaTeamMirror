using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class AquamentusAgent
    {
        private ISprite _sprite;

        private int _health;
        private bool _alive;
        private int _clock;
        private bool _isImmobile;
        private bool _isDying;
        private Point _location;

        public AquamentusAgent(Point location)
        {
            _location = location;
            _alive = true;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateAquamentusIdle();
            _sprite.Hide();
            _isImmobile = true;
            _isDying = false;
        }

        public void Kill()
        {
            if (!_alive)
            {
                return;
            }
            _sprite.Hide();
            _clock = 32;
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            _isDying = true;
            _alive = false;
        }

        public void UseAttack()
        {
            // NO-OP: Attack has no animation
        }

        public void MoveDown()
        {
            if (!_isImmobile)
            {
                _location.Y += 1;
            }
        }

        public void MoveLeft()
        {
            if (!_isImmobile)
            {
                _location.Y -= 1;
            }
        }

        public void MoveRight()
        {
            if (!_isImmobile)
            {
                _location.X += 1;
            }
        }

        public void MoveUp()
        {
            if (!_isImmobile)
            {
                _location.Y -= 1;
            }
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            _clock = 30;
            _health = 10;
            _alive = true;
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
        }

        public void Update()
        {
            if (_clock > 0)
            {
                _clock--;
                if (_clock == 0)
                {
                    CheckFlags();
                }
            }
            _sprite.Update();

        }

        private void CheckFlags()
        {
            if (_isImmobile)
            {
                _sprite = EnemySpriteFactory.Instance.CreateAquamentusIdle();
                _isImmobile = false;
            }

            if (_isDying)
            {
                _sprite = EnemySpriteFactory.Instance.CreateAquamentusIdle();
                _sprite.Hide();
                _isDying = false;
            }
        }
    }
    }
