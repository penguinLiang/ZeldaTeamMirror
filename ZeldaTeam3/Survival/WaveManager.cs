using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Survival.GameState;
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
    public class WaveManager
    {
        private string[][] _waveMatrix;
        private int _currentWave;
        private List<Wave> _waveStorage = new List<Wave>();

        public WaveManager(ContentManager content)
        {
            _waveMatrix = content.Load<string[][]>("SurvivalWaves");
            _currentWave = 0;
            char[] separator = {':'};
            Int32 count = 2;

            for(int row = 0; row < _waveMatrix.Length; row++)
            {
                WaveType currentWaveType = WaveType.Normal;
                List<EnemyType> enemyCSVContent = new List<EnemyType>();
                for(int col = 0; col < _waveMatrix[row].Length; col++)
                {
                    if(col == 0)
                    {
                        currentWaveType = GetCurrentWaveType(row);
                    }
                    else
                    {
                        String[] strList = _waveMatrix[row][col].Split(separator, count, StringSplitOptions.None);
                        int enemyCount = int.Parse(strList[1]);

                        EnemyType enemyType = GetCurrentEnemyType(row, col, strList);

                        for(int currentCount = 0; currentCount < enemyCount; currentCount++)
                        {
                            enemyCSVContent.Add(enemyType);
                        }
                    }
                }

                _waveStorage.Add(new Wave(enemyCSVContent,currentWaveType));

            }
        }

        private WaveType GetCurrentWaveType(int row)
        {
            switch (_waveMatrix[row][0])
            {
                case "S":
                    return WaveType.Shop;
                case "P":
                    return WaveType.Party;
                case "D":
                    return WaveType.Normal;
                default:
                    throw new Exception("Needs a valid wave type!");
            }
        }

        private EnemyType GetCurrentEnemyType(int row, int col, string[] strList)
        {
            switch (strList[0])
            {
                case "aquamentus":
                    return EnemyType.Aquamentus;
                case "gel":
                    return EnemyType.Gel;
                case "goriya":
                    return EnemyType.Goriya;
                case "keese":
                    return EnemyType.Keese;
                case "stalfos":
                    return EnemyType.Stalfos;
                case "wallmaster":
                    return EnemyType.WallMaster;
                case "fygar":
                    return EnemyType.Fygar;
                default:
                    throw new Exception("Needs to be a valid enemy type!");
            }
        }

    }
}
