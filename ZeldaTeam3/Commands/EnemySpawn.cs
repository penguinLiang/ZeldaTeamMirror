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
        static int counter = 0;

        public EnemySpawn(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            IEnemy randomEnemy = _enemy[counter];
            counter++;
            if (counter >= _enemy.Length)
            {
                counter = 0;
            }

            randomEnemy.Spawn();

        }

        public override string ToString() => "Enemy: Spawn";
    }
}
