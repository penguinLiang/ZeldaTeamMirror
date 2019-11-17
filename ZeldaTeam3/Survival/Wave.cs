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
        private List<IEnemy> _waveEnemies = new List<IEnemy>();

        /*
        public int currentSpawnTimer;
        public int waveTime;
        */

        public WaveType Type;
        //public int difficultyScale;

        public Wave(List<IEnemy> enemyCSVContent, WaveType waveType)
        {
            //waveTime = spawnTimer;
            //currentSpawnTimer = waveTime;
            //difficultyScale = 1;
            Type = waveType;

            foreach (var enemy in enemyCSVContent)
            {
                _waveEnemies.Add(enemy);
            }
        }

        public List<IEnemy> getList(int scale)
        {
            List<IEnemy> _unspawnedEnemies = new List<IEnemy>();
            int i = 0;
            foreach(var enemy in _waveEnemies)
            {
                while(i < scale)
                {
                    _unspawnedEnemies.Add(enemy);
                    i++;
                }
                i = 0;
            }
            return _unspawnedEnemies;
        }
    }
}        
/*
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
            
        }*/

        /*
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
                        UnspawnedEnemies.Add(enemy);
                        i++;
                    }
                }
            }
            if(currentSpawnTimer > 0)
            {
                currentSpawnTimer--;
            }
        } */
