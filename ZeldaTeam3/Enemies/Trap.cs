using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class Trap : IEnemy
    {
        private TrapAgent _agent;

        public Trap(SpriteBatch spriteBatch, int posX, int posY)
        {
            _agent = new TrapAgent(spriteBatch, posX, posY);
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
            return;
        }

        public void UseAttack()
        {
            return;
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
