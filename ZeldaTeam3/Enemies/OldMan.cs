using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class OldMan : IEnemy
    {
        private readonly OldManAgent _agent;

        public OldMan(Point location)
        {
            _agent = new OldManAgent(location);
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
