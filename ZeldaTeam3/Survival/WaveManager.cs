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
        private int _scale;
        private List<EnemyType> _currentAliveEnemies;
        private List<Wave> _waveStorage = new List<Wave>();
        private SurvivalRoom _dungeonRoom;

        public WaveManager(SurvivalRoom dungeonRoom, string[][] waveMatrix)
        {
            _dungeonRoom = dungeonRoom;
            _currentWave = 0;
            _scale = 1;
            char[] separator = {':'};
            Int32 count = 2;

            for(int row = 0; row < waveMatrix.Length; row++)
            {
                WaveType currentWaveType = GetCurrentWaveType(waveMatrix[row][0]);
                List<EnemyType> enemyCSVContent = new List<EnemyType>();
                for(int col = 1; col < waveMatrix[row].Length; col++)
                {
                    String[] strList = waveMatrix[row][col].Split(separator, count, StringSplitOptions.None);
                    int enemyCount = int.Parse(strList[1]);

                    EnemyType enemyType = GetCurrentEnemyType(row, col, strList);

                    for(int currentCount = 0; currentCount < enemyCount; currentCount++)
                    {
                        enemyCSVContent.Add(enemyType);
                    }
                }

                _waveStorage.Add(new Wave(enemyCSVContent,currentWaveType));

            }
            _currentAliveEnemies = _waveStorage[_currentWave].GetList(_scale);
        }

        public void Update()
        {
            if(_dungeonRoom.SomeEnemiesAlive == false)
            {
                _currentWave++;
                if(_currentWave == 2) //Currently 2, but change later when there are around 10-20 unique waves
                {
                    _currentWave = 0;
                    _scale++;
                    _currentAliveEnemies = _waveStorage[_currentWave].GetList(_scale);
                }
            }
            foreach(var enemy in _currentAliveEnemies)
            {
                _dungeonRoom.SpawnEnemy((int)enemy);
            }
            _currentAliveEnemies.Clear();
        }

        private static WaveType GetCurrentWaveType(string type)
        {
            switch (type)
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
