using System.Collections.Generic;
using Zelda.Dungeon;

// ReSharper disable ParameterTypeCanBeEnumerable.Local
namespace Zelda.Survival
{
    public class Wave
    {
        private readonly List<EnemyType> _waveEnemies = new List<EnemyType>();
        public WaveType Type { get; }

        public Wave(List<EnemyType> enemyTypes, WaveType waveType)
        {
            Type = WaveType.Shop;

            foreach (var enemy in enemyTypes)
            {
                _waveEnemies.Add(enemy);
            }
        }

        public EnemyType[] GetList(int scale)
        {
            var enemies = new EnemyType[scale * _waveEnemies.Count];

            for (var i = 0; i < _waveEnemies.Count; i++)
            {
                var enemyType = _waveEnemies[i];
                for(var j = 0; j < scale; j++)
                {
                    enemies[i * scale + j] = enemyType;
                }
            }

            return enemies;
        }
    }
}        
