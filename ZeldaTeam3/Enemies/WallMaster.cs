using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class WallMaster : IEnemy
    {
        private readonly WallMasterAgent _agent;

        public WallMaster(Point location)
        {
            _agent = new WallMasterAgent(location);
        }

        public bool Alive { get; }

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

        public void Knockback()
        {
            throw new System.NotImplementedException();
        }

        public void Halt()
        {
            throw new System.NotImplementedException();
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

        public void Stun()
        {
            throw new System.NotImplementedException();
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
