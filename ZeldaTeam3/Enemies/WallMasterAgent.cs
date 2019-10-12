using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class WallMasterAgent
    {
        private readonly ISprite _sprite;

        private StatusHealth _statusHealth;

        private int _health;
        private Point _location;

        private enum StatusHealth
        {
            Alive, Dead
        }

        public WallMasterAgent(Point location)
        {
            _location = location;
            _statusHealth = StatusHealth.Alive;
            _health = 0;
            _sprite = EnemySpriteFactory.Instance.CreateWallMaster();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
            _statusHealth = StatusHealth.Dead;
        }

        public void UseAttack()
        {
            // NO-OP: Attack has no animation
        }

        public void MoveDown()
        {
            _location.Y += 1;
        }

        public void MoveLeft()
        {
            _location.Y -= 1;
        }

        public void MoveRight()
        {
            _location.X += 1;
        }

        public void MoveUp()
        {
            _location.Y -= 1;
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
            _sprite.Draw(_location.ToVector2());
        }

        public void Update()
        {
            _sprite.Update();
        }
    }
}