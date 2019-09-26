using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Commands
{
    class EnemySpawn : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemySpawn(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.Spawn();
            }

        }

        public override string ToString() => "Enemy: Spawn";
    }
}
