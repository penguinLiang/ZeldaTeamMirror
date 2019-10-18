using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Gel : EnemyAgent
    {
        private readonly GelAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X + 4, _agent.Location.Y + 7, 8, 9);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateGel();
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
