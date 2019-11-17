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

        public WaveType Type;

        public Wave(List<IEnemy> enemyCSVContent, WaveType waveType)
        {
            Type = waveType;

            foreach (var enemy in enemyCSVContent)
            {
                _waveEnemies.Add(enemy);
            }
        }

        public List<IEnemy> GetList(int scale)
        {
            List<IEnemy> _unspawnedEnemies = new List<IEnemy>();
            for(int i = 0; i < scale; i++)
            {
                foreach(var enemy in _waveEnemies)
                {
                    _unspawnedEnemies.Add(enemy);
                }
            }
            return _unspawnedEnemies;
        }
    }
}        
