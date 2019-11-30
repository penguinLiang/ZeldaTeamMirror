using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Dungeon;
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable InvertIf

namespace Zelda.Survival
{
    public class WaveManager : IUpdatable
    {
        public int CurrentWave { get; private set; }
        private int _scale = 1;
        private readonly List<Wave> _waveStorage = new List<Wave>();

        private readonly FrameDelay _spawnDelay = new FrameDelay(60, true);
        private List<Point> _spawnLocations;

        private EnemyType[] _waveEnemyTypes;
        private int _currentEnemyOffset;
        private bool _waveStarted;

        private uint _spawnCount;

        public List<IEnemy> Enemies { get; } = new List<IEnemy>();

        public WaveManager(string[][] waveMatrix)
        {
            for(var row = 0; row < waveMatrix.Length; row++)
            {
                var currentWaveType = DecodeWaveType(waveMatrix[row][0]);
                var enemyTypes = new List<EnemyType>();

                for(var col = 1; col < waveMatrix[row].Length; col++)
                {
                    var strList = waveMatrix[row][col].Split(':');
                    var enemyCount = int.Parse(strList[1]);
                    var enemyType = DecodeEnemyType(strList[0]);

                    for (var currentCount = 0; currentCount < enemyCount; currentCount++)
                    {
                        enemyTypes.Add(enemyType);
                    }
                }

                _waveStorage.Add(new Wave(enemyTypes, currentWaveType));
            }
        }

        private static WaveType DecodeWaveType(string type)
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

        private static EnemyType DecodeEnemyType(string enemyString)
        {
            switch (enemyString)
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

        public WaveType CurrentWaveType => _waveStorage[CurrentWave].Type;

        public void AdvanceWave()
        {
            if (++CurrentWave == _waveStorage.Count)
            {
                CurrentWave = 0;
                _scale++;
            }
        }

        private EnemyType[] NextWaveEnemies()
        {
            return _waveStorage[CurrentWave].GetList(_scale);
        }

        public void ClearWave()
        {
            _currentEnemyOffset = 0;
            Enemies.Clear();
            _waveStarted = false;
            _spawnDelay.Pause();
        }

        public void StartEnemyWave()
        {
            _waveStarted = true;
            _waveEnemyTypes = NextWaveEnemies();
            _spawnDelay.Resume();
        }

        public bool SomeEnemiesAlive => Enemies.Any(enemy => enemy.Alive) || _currentEnemyOffset < _waveEnemyTypes.Length;
        public bool WaveComplete => _waveStarted && !SomeEnemiesAlive;

        public void TrackSpawnsNearPlayer(List<Point> spawnTiles, Point location)
        {
            if (_spawnDelay.Delayed) return;

            _spawnLocations = ZoneManager.SpawnTilesWithinZone(spawnTiles, location);
        }

        public void Reset()
        {
            CurrentWave = 0;
            _scale = 1;
            ClearWave();
        }

        public void Update()
        {
            if (!_waveStarted || WaveComplete) return;

            _spawnDelay.Update();

            if (_spawnDelay.Delayed ||
                _spawnLocations == null ||
                _spawnLocations.Count == 0 ||
                _currentEnemyOffset >= _waveEnemyTypes.Length) return;

            var roundRobin = (int)(_spawnCount % _spawnLocations.Count);
            var spawnLocation = _spawnLocations[roundRobin];
            var enemy = EnemyFactory.MakeEnemy(spawnLocation, _waveEnemyTypes[_currentEnemyOffset]);
            Enemies.Add(enemy);
            enemy.Spawn();
            _currentEnemyOffset++;
            _spawnCount++;
        }
    }
}
