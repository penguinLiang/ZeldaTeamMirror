using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class OldManAgent
    {
        private readonly ISprite _sprite;

        private int _posX;
        private int _posY;

        public OldManAgent(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
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
