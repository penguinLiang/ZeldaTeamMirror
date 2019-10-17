using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Gel : Enemy
    {
        private readonly GelAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
        public override bool Alive => _agent.Alive;

        public Gel(Point location)
        {
            _agent = new GelAgent(location);
        }

        public override void Spawn()
        {
            _agent.Spawn();
        }

        public override void TakeDamage()
        {
            _agent.TakeDamage();
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
