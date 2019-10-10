using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class StalfosAgent
    {
        private readonly ISprite _sprite;

        private StatusHealth _statusHealth;

        private int _health;
        private int _posX;
        private int _posY;

        private enum StatusHealth
        {
            Alive, Dead
        }

        public StalfosAgent(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _statusHealth = StatusHealth.Alive;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateStalfos();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
            _statusHealth = StatusHealth.Dead;
        }

        public void MoveDown()
        {
            _posY += 1;
        }

        public void UseAttack()
        {
            // NO-OP: Attack has no animation
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
            _sprite.Show();
            _health = 10;
            _statusHealth = StatusHealth.Alive;
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
            _sprite.Draw(new Vector2(_posX, _posY));
        }

        public void Update()
        {
            _sprite.Update();
        }
    }
}