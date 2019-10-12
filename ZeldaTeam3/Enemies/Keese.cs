using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Keese : IEnemy
    {
        private readonly KeeseAgent _agent;

        public Keese(Point location)
        {
            _agent = new KeeseAgent(location);
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
            // NO-OP: Attack has no animation
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
