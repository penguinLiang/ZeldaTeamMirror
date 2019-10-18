using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    internal class Goriya : EnemyAgent
    {
        private readonly GoriyaAgent _agent;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateGoriyaFaceDown();
        public override bool Alive => _agent.Alive;

        public Goriya(Point location)
        {
            _agent = new GoriyaAgent(location);
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
