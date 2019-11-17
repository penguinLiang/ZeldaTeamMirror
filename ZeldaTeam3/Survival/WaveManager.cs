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

            for(int i = 0; i < _waveMatrix.Length; i++)
            {
                WaveType currentWaveType = WaveType.Normal;
                List<EnemyType> enemyCSVContent = new List<EnemyType>();
                for(int j = 0; j < _waveMatrix[i].Length; j++)
                {
                    if(j == 0)
                    {
                        switch(_waveMatrix[i][0])
                        {
                            case "S":
                                currentWaveType = WaveType.Shop;
                                break;
                            case "P":
                                currentWaveType = WaveType.Party;
                                break;
                            case "N":
                                currentWaveType = WaveType.Normal;
                                break;
                            default:
                                throw new Exception("Needs a valid wave type!");
                        }
                    }

                    else
                    {
                        String[] strList = _waveMatrix[i][j].Split(separator, count, StringSplitOptions.None);
                        int enemyCount = int.Parse(strList[1]);
                        EnemyType EnemyType = EnemyType.Gel;
                        switch(strList[0])
                        {
                            case "aquamentus":
                                EnemyType = EnemyType.Aquamentus;
                                break;
                            case "gel":
                                EnemyType = EnemyType.Gel;
                                break;
                            case "goriya":
                                EnemyType = EnemyType.Goriya;
                                break;
                            case "keese":
                                EnemyType = EnemyType.Keese;
                                break;
                            case "stalfos":
                                EnemyType = EnemyType.Stalfos;
                                break;
                            case "wallmaster":
                                EnemyType = EnemyType.WallMaster;
                                break;
                            case "fygar":
                                EnemyType = EnemyType.Fygar;
                                break;
                            default:
                                throw new Exception("Needs to be a valid enemy type!");
                        }

                        for(int k = 0; k < enemyCount; k++)
                        {
                            enemyCSVContent.Add(EnemyType);
                        }
                    }
                }

                _waveStorage.Add(new Wave(enemyCSVContent,currentWaveType));

            }
        }


    }
}
