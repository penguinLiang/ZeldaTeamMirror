using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Trap : IEnemy
    {
        private readonly TrapAgent _agent;

        public Trap(Point location)
        {
            _agent = new TrapAgent(location);
        }

        public bool Alive { get; } = true;

        public void Spawn()
        {
            _agent.Spawn();
        }

        public void TakeDamage()
        {
            // NO-OP: Trap cannot take damage
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

        public void Stun()
        {
            throw new System.NotImplementedException();
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

        public void Knockback()
        {
            throw new System.NotImplementedException();
        }

        public void Halt()
        {
            throw new System.NotImplementedException();
        }
    }
}
