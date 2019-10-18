using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Trap : EnemyAgent
    {
        private readonly TrapAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateTrap();
        public override bool Alive => true;

        public Trap(Point location)
        {
            _agent = new TrapAgent(location);
        }

        public override void Spawn()
        {
            _agent.Spawn();
        }

        public override void TakeDamage()
        {
            //No-Op: Can't be damaged
        }

        public override void Stun()
        {
            throw new System.NotImplementedException();
        }

        public override void Draw()
        {
            _agent.Draw();
        }

        public override void Update()
        {
            _agent.Update();
        }

        public override void Knockback()
        {
            throw new System.NotImplementedException();
        }

        public override void Halt()
        {
            _agent.Halt();
        }
    }
}
