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
        private readonly List<EnemyType> _waveEnemies = new List<EnemyType>();

        public WaveType Type { get; }

        public Wave(List<EnemyType> enemyCSVContent, WaveType waveType)
        {
            Type = waveType;

            foreach (var enemy in enemyCSVContent)
            {
                _waveEnemies.Add(enemy);
            }
        }

        public List<EnemyType> GetList(int scale)
        {
            var unspawnedEnemies = new List<EnemyType>();

            for(var i = 0; i < scale; i++)
            {
                unspawnedEnemies.AddRange(_waveEnemies);
            }

            return unspawnedEnemies;
        }
    }
}        
