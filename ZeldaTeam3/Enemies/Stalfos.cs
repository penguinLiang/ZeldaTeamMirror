using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class Stalfos : IEnemy
    {
        private StalfosAgent _agent;

        public Stalfos(SpriteBatch spriteBatch, int posX, int posY)
        {
            _agent = new StalfosAgent(spriteBatch, posX, posY);
        }

        public void FaceDown()
        {
            return;
        }

        public void FaceLeft()
        {
            return;
        }

        public void FaceRight()
        {
            return;
        }

        public void FaceUp()
        {
            return;
        }

        public void Idle()
        {
            return;
        }

        public void Kill()
        {
            _agent.Kill();
        }

        public void MoveDown()
        {
            _agent.MoveDown();
        }

        public void MoveLeft()
        {
            _agent.MoveLeft();
        }

        public void MoveRight()
        {
            _agent.MoveRight();
        }

        public void MoveUp()
        {
            _agent.MoveUp();
        }

        public void Spawn()
        {
            _agent.Spawn();
        }

        public void TakeDamage()
        {
            _agent.TakeDamage();
        }

        public void UseAttack()
        {
            _agent.UseAttack();
        }

        public void Draw()
        {
            _agent.Draw();
        }

        public void Update()
        {
            _agent.Update();
        }
    }
}
