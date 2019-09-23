using System;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class StalfosStateMachine
    {
        private Stalfos _stalfos;
        private ISprite _sprite;

        private int _health;
        private StatusHealth _statusHealth;
        private int _posX;
        private int _posY;

        private enum StatusHealth
        {
            Alive, Dead
        };

        public StalfosStateMachine(Stalfos stalfos, int posX, int posY)
        {
            _posX = posX;
            _posY = posY;
            _stalfos = stalfos;
            _statusHealth = StatusHealth.Dead;
        }

        internal void Kill()
        {
            _statusHealth = StatusHealth.Dead;
        }

        internal void MoveDown()
        {
            throw new NotImplementedException();
        }

        internal void UseAttack()
        {
            throw new NotImplementedException();
        }

        internal void MoveLeft()
        {
            throw new NotImplementedException();
        }

        internal void MoveRight()
        {
            throw new NotImplementedException();
        }

        internal void MoveUp()
        {
            throw new NotImplementedException();
        }

        internal void Spawn()
        {
            throw new NotImplementedException();
        }

        internal void TakeDamage()
        {
            throw new NotImplementedException();
        }

        internal void Draw()
        {

        }

        internal void Update()
        {

        }
    }
}