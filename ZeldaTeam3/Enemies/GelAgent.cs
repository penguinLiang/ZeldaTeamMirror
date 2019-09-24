using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class GelAgent
    {
        private Gel _gel;
        private readonly ISprite _sprite;

        private StatusHealth _statusHealth;

        private Direction _statusDirection;

        private Vector2 _previousLocation;

        private int _health;
        private int _posX;
        private int _posY;

        private readonly float GAP = 1;

        private readonly SpriteBatch _spriteBatch;

        private enum StatusHealth
        {
            Alive, Dead, Damaged
        };


        public GelAgent(Gel gel, SpriteBatch spriteBatch, int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _gel = gel;
            _statusHealth = StatusHealth.Dead;
            _spriteBatch = spriteBatch;
            _previousLocation = new Vector2(posX, posY);
            _sprite = EnemySpriteFactory.Instance.CreateGel();
        }

        public void Kill()
        {
            _statusHealth = StatusHealth.Dead;
        }

        public void MoveDown()
        {
            _posY += 1;
        }

        public void UseAttack()
        {
            return;
        }

        public void MoveLeft()
        {
            _posX -= 1;
        }

        public void MoveRight()
        {
            _posX += 1;
        }

        public void MoveUp()
        {
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
            switch (_statusHealth)
            {
                case StatusHealth.Damaged:
                    _sprite.PaletteShift();
                    break;
                case StatusHealth.Dead:
                    _sprite.Hide();
                    break;
                default:
                    _sprite.Show();
                    break;
            }

            _sprite.Update();
        }

        private void UpdateMoving(Direction direction)
        {
            _statusDirection = direction;
        }
    }
}