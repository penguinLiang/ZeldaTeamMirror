using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;

// ReSharper disable SwitchStatementMissingSomeCases (handled at runtime)
namespace Zelda.Survival
{
    public sealed class SurvivalRoom : Room
    {
        private readonly Random _rnd = new Random((int)DateTime.Now.Ticks);

        private readonly List<Point> _spawnTiles = new List<Point>();
        private List<Point> _availableSpawnTiles = new List<Point>();

        private readonly EnemyType _enemyType;
        private readonly SurvivalManager _survivalManager;

        // ReSharper disable once SuggestBaseTypeForParameter (the input must be a jagged int array)
        public SurvivalRoom(SurvivalManager manager, int[][] tiles):base(manager, tiles, (int)EnemyType.None)
        {
            _survivalManager = manager;
            //TODO: IMPLEMENT THIS LOGIC AND REMOVE THIS LINE
            _availableSpawnTiles = _spawnTiles;
        }

        public void SpawnEnemy(int enemyId)
        {
            //TODO: To be called by WaveManager or something. Should only spawn on the tiles within the current zone.
            //Update to control the zones that the player has unlocked.
            //currently we have an array of all the spawn tiles, this can spawn all the waves.
            if (_spawnTiles.Count > 0)
            {
                var enemy = MakeEnemy(enemyId, _spawnTiles[_rnd.Next(_spawnTiles.Count)]);
                Enemies.Add(enemy);
                enemy.Spawn();
            }
        }

        private IEnemy MakeEnemy(int enemyId, Point spawnPoint)
        {
            
            switch ((EnemyType) enemyId)
            {
                case EnemyType.Gel:
                    return new Gel(spawnPoint);
                case EnemyType.Goriya:
                    return new Goriya(spawnPoint);
                case EnemyType.Keese:
                    return new Keese(spawnPoint);
                case EnemyType.OldMan:
                    return new OldMan(spawnPoint);
                case EnemyType.Stalfos:
                    return new Stalfos(spawnPoint);
                case EnemyType.Trap:
                    return new Trap(spawnPoint);
                case EnemyType.WallMaster:
                    return new WallMaster(spawnPoint);
                case EnemyType.Aquamentus:
                    return new Aquamentus(spawnPoint);
                case EnemyType.Fygar:
                    return new Fygar(spawnPoint);
                case EnemyType.None:
                    throw new Exception("Room spawns an enemy but no type is set");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool TryAddNonStandardTiles(MapTile tile, Point location) {
            // ReSharper disable once SwitchStatementMissingSomeCases (cases are covered elsewhere)
            switch (tile)
            {
                case MapTile.Key:
                    Items.Add(new Key(location, this));
                    break;
                case MapTile.Compass:
                    Items.Add(new Compass(location));
                    break;
                case MapTile.Map:
                    Items.Add(new Map(location));
                    break;
                case MapTile.Bow:
                    Items.Add(new BowItem(location, Secondary.Bow));
                    break;
                case MapTile.Triforce:
                    Items.Add(new Triforce(location));
                    break;
                case MapTile.Room2_1Block:
                    var room21Block = new Room2_1Block(this, location);
                    Collidables.Add(room21Block);
                    Drawables.Add(room21Block);
                    TransitionResetables.Add(room21Block);
                    break;
                case MapTile.PushableBlock:
                    var pushableBlock = new MovableBlock(location);
                    Collidables.Add(pushableBlock);
                    Drawables.Add(pushableBlock);
                    TransitionResetables.Add(pushableBlock);
                    break;
                case MapTile.SpawnEnemy:
                    _spawnTiles.Add(location);
                    break;
                case MapTile.Sand:
                    Drawables.Add(new Overlay(location, BlockType.Sand));
                    break;
                case MapTile.Heart:
                    Items.Add(new HeartContainer(location, this));
                    break;
                case MapTile.Boomerang:
                    Items.Add(new BoomerangItem(location, this));
                    break;
                case MapTile.BasementBricks:
                case MapTile.BlackOverlay:
                case MapTile.None:
                    break;
                default:
                    return false;
            }

            return true;
        }


    }
}