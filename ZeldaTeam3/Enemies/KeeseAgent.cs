using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class KeeseAgent
    {
        private readonly ISprite _sprite;

        private Point _location;

        public KeeseAgent(Point location)
        {
            _location = location;
            _sprite = EnemySpriteFactory.Instance.CreateKeese();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
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
            _location.X -= 1;
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
        }

        public void TakeDamage()
        {
            Kill();
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