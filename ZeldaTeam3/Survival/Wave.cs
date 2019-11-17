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

        public bool SpawnEnemy()
        {
            if(currentSpawnTimer <= 0)
            {
                currentSpawnTimer = waveTime;
                return true;
            } else
            {
                return false;
            }
            
        }

        public void EndWave()
        {
            UnspawnedEnemies.Clear();
            difficultyScale++;
            foreach (var enemy in WaveEnemies)
            {
                int i = 0;
                while(i < difficultyScale)
                {
                    UnspawnedEnemies.AddLast(enemy);
                    i++;
                }
            }
        }

        public void Update()
        {
            if(currentSpawnTimer > 0)
            {
                currentSpawnTimer--;
            }
        }
    }
}
