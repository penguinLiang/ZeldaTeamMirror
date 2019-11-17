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
                        currentWaveType = _getCurrentWaveType(row);
                    }
                    else
                    {
                        String[] strList = _waveMatrix[row][col].Split(separator, count, StringSplitOptions.None);
                        int enemyCount = int.Parse(strList[1]);

                        EnemyType enemyType = _getCurrentEnemyType(row, col, strList);

                        for(int currentCount = 0; currentCount < enemyCount; currentCount++)
                        {
                            enemyCSVContent.Add(enemyType);
                        }
                    }
                }

                _waveStorage.Add(new Wave(enemyCSVContent,currentWaveType));

            }
        }

        private WaveType _getCurrentWaveType(int row)
        {
            WaveType currentWaveType = WaveType.Normal;
            switch (_waveMatrix[row][0])
            {
                case "S":
                    currentWaveType = WaveType.Shop;
                    break;
                case "P":
                    currentWaveType = WaveType.Party;
                    break;
                case "D":
                    currentWaveType = WaveType.Normal;
                    break;
                default:
                    throw new Exception("Needs a valid wave type!");
            }
            return currentWaveType;
        }

        private EnemyType _getCurrentEnemyType(int row, int col, string[] strList)
        {
            EnemyType enemyType = EnemyType.None;
            switch (strList[0])
            {
                case "aquamentus":
                    enemyType = EnemyType.Aquamentus;
                    break;
                case "gel":
                    enemyType = EnemyType.Gel;
                    break;
                case "goriya":
                    enemyType = EnemyType.Goriya;
                    break;
                case "keese":
                    enemyType = EnemyType.Keese;
                    break;
                case "stalfos":
                    enemyType = EnemyType.Stalfos;
                    break;
                case "wallmaster":
                    enemyType = EnemyType.WallMaster;
                    break;
                case "fygar":
                    enemyType = EnemyType.Fygar;
                    break;
                default:
                    throw new Exception("Needs to be a valid enemy type!");
            }
            return enemyType;
        }

    }
}
