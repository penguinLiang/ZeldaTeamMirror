using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public class OldMan : Enemy
    {
        private readonly OldManAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
        public override bool Alive => true;

        public OldMan(Point location)
        {
            _agent = new OldManAgent(location);
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
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
