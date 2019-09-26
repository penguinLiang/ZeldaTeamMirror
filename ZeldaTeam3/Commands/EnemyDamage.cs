using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Commands
{
    class EnemyDamage : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyDamage(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.TakeDamage();
            }
        }

        public override string ToString() => "Enemy: Take damage";
    }
}
