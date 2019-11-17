using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.GameState;
using Zelda.HighScore;
using Zelda.HUD;
using Zelda.Items;
using Zelda.JumpMap;
using Zelda.Music;
using Zelda.Pause;
using Zelda.Player;
using Zelda.Projectiles;
using Zelda.SoundEffects;

namespace Zelda.Survival
{
    public class Wave
    {
        public LinkedList<IEnemy> WaveEnemies = new LinkedList<IEnemy>();
        public LinkedList<IEnemy> UnspawnedEnemies = new LinkedList<IEnemy>();
        public int currentSpawnTimer;
        public int waveTime;

        public WaveType Type;
        public int difficultyScale;

        public Wave(LinkedList<IEnemy> enemyCSVContent, int spawnTimer, WaveType waveType)
        {
            waveTime = spawnTimer;
            currentSpawnTimer = waveTime;
            difficultyScale = 1;
            Type = waveType;

            foreach(var enemy in enemyCSVContent)
            {
                WaveEnemies.AddLast(enemy);
            }

            foreach (var enemy in WaveEnemies)
            {
                UnspawnedEnemies.AddLast(enemy);
            }
        }

        public int CanSpawnEnemy()
        {
            // There is still enemies left, and spawn timer is ready to spawn, return 0
            if(currentSpawnTimer == 0 && UnspawnedEnemies.Count != 0)
            {
                currentSpawnTimer = waveTime;
                return 0;
            }

            // There is no more enemies, the wave is over, so return 1
            if(UnspawnedEnemies.Count == 0)
            {
                return 1;
            }

            // There are still enemies, but spawn is still on cooldown, return 2
            return 2;
            
        }

        public void Update()
        {
            if(UnspawnedEnemies.Count == 0)
            {
                difficultyScale++;
                foreach (var enemy in WaveEnemies)
                {
                    int i = 0;
                    while (i < difficultyScale)
                    {
                        UnspawnedEnemies.AddLast(enemy);
                        i++;
                    }
                }
            }
            if(currentSpawnTimer > 0)
            {
                currentSpawnTimer--;
            }
        }
    }
}
