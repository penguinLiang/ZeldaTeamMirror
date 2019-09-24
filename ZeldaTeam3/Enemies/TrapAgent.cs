using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class TrapAgent
    {
        private Trap _trap;
        private readonly ISprite _sprite;

        private StatusHealth _statusHealth;

        private int _posX;
        private int _posY;

        private readonly SpriteBatch _spriteBatch;

        private enum StatusHealth
        {
            Alive, Dead
        };


        public TrapAgent(Trap trap, SpriteBatch spriteBatch, int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _trap = trap;
            _statusHealth = StatusHealth.Dead;
            _spriteBatch = spriteBatch;
            _sprite = EnemySpriteFactory.Instance.CreateTrap();
        }

        public void Kill()
        {
            _statusHealth = StatusHealth.Dead;
        }

        public void MoveDown()
        {
            _posY += 1;
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

        public void Draw()
        {
            _sprite.Draw(_spriteBatch, new Vector2(_posX, _posY));
        }

        public void Update()
        {
            _sprite.Update();
        }
    }
}