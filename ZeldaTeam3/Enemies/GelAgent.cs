﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class GelAgent
    {
        private readonly ISprite _sprite;

        private int _posX;
        private int _posY;

        private readonly SpriteBatch _spriteBatch;


        public GelAgent(SpriteBatch spriteBatch, int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _spriteBatch = spriteBatch;
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
            _sprite.Show();
        }

        public void TakeDamage()
        {
            this.Kill();
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