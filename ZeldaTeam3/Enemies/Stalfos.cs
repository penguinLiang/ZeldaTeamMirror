using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Enemies
{
    public class Stalfos : IEnemy
    {
        private StalfosStateMachine _stateMachine;

        public ISprite Sprite { get; set; }

        public Stalfos()
        {
            _stateMachine = new StalfosStateMachine();
            Sprite = EnemySpriteFactory.Instance.CreateStalfos();
        }

        public void FaceDown()
        {
            return;
        }

        public void FaceLeft()
        {
            return;
        }

        public void FaceRight()
        {
            return;
        }

        public void FaceUp()
        {
            return;
        }

        public void Idle()
        {
            throw new NotImplementedException();
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new NotImplementedException();
        }

        public void UseAttack()
        {
            return;
        }
    }
}
