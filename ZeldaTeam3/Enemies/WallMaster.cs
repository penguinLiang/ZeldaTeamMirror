using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class WallMaster : Enemy
    {
        private readonly WallMasterAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
        public override bool Alive => _agent.Alive;

        public WallMaster(Point location)
        {
            _agent = new WallMasterAgent(location);
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

        public override void Halt()
        {
            _agent.Halt();
        }

        public override void Knockback()
        {
            _agent.Knockback();
        }
    }
}
