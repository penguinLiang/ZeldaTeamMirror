using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class GelAgent
    {
        private ISprite _sprite;

        private int _posX;
        private int _posY;
        private bool _isImmobile;
        private bool _isDying;
        private bool _alive;
        private int _clock;

        private readonly SpriteBatch _spriteBatch;


        public GelAgent(SpriteBatch spriteBatch, int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _spriteBatch = spriteBatch;
            _sprite = EnemySpriteFactory.Instance.CreateGel();
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

        public void MoveDown()
        {
            if (!_isImmobile)
            {
                _posY += 1;
            }
        }

        public void UseAttack()
        {
            // NO-OP: Attack has no animation
        }

        public void MoveLeft()
        {
            if (!_isImmobile)
            {
                _posX -= 1;
            }
        }

        public void MoveRight()
        {
            if (!_isImmobile)
            {
                _posX += 1;
            }
        }

        public void MoveUp()
        {
            if (!_isImmobile)
            {
                _posY -= 1;
            }
        }

        public void Spawn()
        {
            _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _isImmobile = true;
            _clock = 30;
            _alive = true;
        }

        public void TakeDamage()
        {
            Kill();
        }

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, new Vector2(_posX, _posY));
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
                _sprite = EnemySpriteFactory.Instance.CreateGel();
                _isImmobile = false;
            }

            if (_isDying)
            {
                _sprite = EnemySpriteFactory.Instance.CreateGel();
                _sprite.Hide();
                _isDying = false;
            }
        }
    }
}