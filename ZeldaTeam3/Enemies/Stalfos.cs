using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Stalfos : IEnemy
    {
        private readonly StalfosAgent _agent;

        public Rectangle Bounds => new Rectangle(_agent.Location.X, _agent.Location.Y, 16, 16);

        public Stalfos(Point location)
        {
            _agent = new StalfosAgent(location);
        }

        public bool Alive { get; } = true;

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

        public void Knockback()
        {
            throw new System.NotImplementedException();
        }

        public void Halt()
        {
            throw new System.NotImplementedException();
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
            _agent.TakeDamage();
        }

        public void Stun()
        {
            throw new System.NotImplementedException();
        }

        public void UseAttack()
        {
            _agent.UseAttack();
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
