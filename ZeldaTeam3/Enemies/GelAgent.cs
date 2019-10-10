using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class GelAgent
    {
        private readonly ISprite _sprite;

        private int _posX;
        private int _posY;

        public GelAgent(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _sprite = EnemySpriteFactory.Instance.CreateGel();
            _sprite.Hide();
        }

        public void Kill()
        {
            _sprite.Hide();
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

        public void TakeDamage()
        {
            Kill();
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