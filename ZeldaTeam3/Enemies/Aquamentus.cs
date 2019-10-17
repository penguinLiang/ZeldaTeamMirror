using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Aquamentus : Enemy
    {
        private readonly AquamentusAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 24, 32);
        public override bool Alive => _agent.Alive;

        public Aquamentus(Point location)
        {
            _agent = new AquamentusAgent(location);
        }

        public override void Spawn()
        {
            _agent.Spawn();
        }

        public override void TakeDamage()
        {
            _agent.TakeDamage();
        }

        public void UseAttack()
        {
            _agent.UseAttack();
        }

        public override void Draw()
        {
            _agent.Draw();
        }

        public override void Update()
        {
            _agent.Update();
        }
    }
}
