namespace Zelda.Enemies
{
    public class Trap : IEnemy
    {
        private readonly TrapAgent _agent;

        public Trap(int posX, int posY)
        {
            _agent = new TrapAgent(posX, posY);
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
            // NO-OP: Trap cannot take damage
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
