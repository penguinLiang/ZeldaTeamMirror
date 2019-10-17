using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class OldManAgent
    {
        private readonly ISprite _sprite;

        public Point Location;

        public OldManAgent(Point location)
        {
            Location = location;
            _sprite = EnemySpriteFactory.Instance.CreateOldMan();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
        }

        public void TakeDamage()
        {
            _sprite.PaletteShift();
        }

        public void UseAttack()
        {
            // NO-OP: Attack has no animation
        }

        public void MoveDown()
        {
            Location.Y += 1;
        }

        public void MoveLeft()
        {
            Location.X -= 1;
        }

        public void MoveRight()
        {
            Location.X += 1;
        }

        public void MoveUp()
        {
            Location.Y -= 1;
        }

        public void Spawn()
        {
            _sprite.Show();
        }

        public void Draw()
        {
            _sprite.Draw(Location.ToVector2());
        }

        public void Update()
        {
            _sprite.Update();
        }
    }
}
