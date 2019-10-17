using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    internal class Goriya : Enemy
    {
        private readonly GoriyaAgent _agent;
        public Projectiles.GoriyaBoomerang Boomerang => _agent.Boomerang;
        public override Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);
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
