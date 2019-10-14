using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Blocks;
using Zelda;

namespace Zelda
{
    class DungeonRoom
    {

        private enum EnemyType
        {
            None = -1,
            Aquamentus = 0,
            Gel = 1,
            Goriya = 2,
            Keese = 3,
            OldMan = 4,
            Stalfos = 5,
            Trap = 6,
            WallMaster = 7
        }

        private enum MapTiles {
            None = -1,

            // Misc
            SpawnEnemy = 0,
            BlackOverlay = 1,
            DungeonStairs = 2,
            BasementStairs = 3,

            // Barriers
            InvisibleWall = 8,
            Fire = 9,
            Water = 10,
            Sand = 11,
            FishStatue = 12,
            DragonStatue = 13,
            BasementBricks = 14,
            BlackBarrier = 15,

            // Items
            Heart = 16,
            Key = 17,
            Map = 18,
            Bow = 19,
            Triforce = 20,
            Compass = 21,
            Boomerang = 22,

            // Blocks
            ImmovableBlock = 24,
            PushableBlock = 25,
            Room2_1Block = 26,

            // Doors
            DoorUp = 32,
            DoorDown = 33,
            DoorRight = 34,
            DoorLeft = 35,

            // Locked Doors
            DoorLockedUp = 40,
            DoorLockedLeft = 41,
            DoorLockedDown = 42,
            DoorLockedRight = 43,

            // Bombable Doors
            DoorBombableUp = 48,
            DoorBombableLeft = 49,
            DoorBombableDown = 50,
            DoorBombableRight = 51,

            // Special Doors
            DoorSpecialLeft2_1 = 56,
            DoorSpecialRight3_1 = 57,
            DoorSpecialUp1_1 = 58
        }

        private int[][] _tiles;

        private SpriteBatch _spriteBatch;

        private EnemyType _enemyType;
        private List<IEnemy> _enemyStorage;

        public DungeonRoom(int[][] tiles, int enemyID, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _tiles = tiles;
            _enemyType = (EnemyType)enemyID;
            InitializeAllEnemies();
            InitializeAllTiles();
        }

        public void DecrementEnemy(int enemiesKilled)
        {
            for (int i = 0; i < enemiesKilled; i++)
            {
                _enemyStorage.RemoveAt(0);
            }
        }

        private void InitializeAllTiles() 
        {
            for (int i = 0; i<16; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    int _spawnCoordX = i * 16;
                    int _spawnCoordY = j * 16;
                    Point spawnPointXY = new Point(_spawnCoordX, _spawnCoordY);
                    switch ((MapTiles)_tiles[i][j])
                    {
                        case MapTiles.DungeonStairs:
                            DoorsAndStairs dungeonStairs = new DoorsAndStairs(spawnPointXY, BlockType.DungeonStair);
                            break;
                        case MapTiles.BasementStairs:
                            DoorsAndStairs basementStairs = new DoorsAndStairs(spawnPointXY, BlockType.BasementStair);
                            break;
                        case MapTiles.InvisibleWall:
                            Barrier invisWall = new Barrier(spawnPointXY, BlockType.InvisibleBlock);
                            break;
                        case MapTiles.Fire:
                            Barrier fire = new Barrier(spawnPointXY, BlockType.Fire);
                            break;
                        case MapTiles.Water:
                            Barrier water = new Barrier(spawnPointXY, BlockType.Water);
                            break;
                        case MapTiles.FishStatue:
                            Barrier fishStatue = new Barrier(spawnPointXY, BlockType.FishStatue);
                            break;
                        case MapTiles.DragonStatue:
                            Barrier dragonStatue = new Barrier(spawnPointXY, BlockType.DragonStatue);
                            break;
                        case MapTiles.Key:
                            Key key = new Key(spawnPointXY);
                            break;
                        case MapTiles.Compass:
                            Compass compass = new Compass(spawnPointXY);
                            break;
                        case MapTiles.Map:
                            Map map = new Map(spawnPointXY);
                            break;
                        case MapTiles.Bow:
                            BowItem bow = new BowItem(spawnPointXY);
                            break;
                        case MapTiles.Triforce:
                            Triforce triforce = new Triforce(spawnPointXY);
                            break;
                        case MapTiles.ImmovableBlock:
                            Barrier immovableGeneric = new Barrier(spawnPointXY, BlockType.ImmovableBlock);
                            break;
                        case MapTiles.Room2_1Block:
                            MovableBlock room2_1Block = new MovableBlock(spawnPointXY);
                            break;
                        case MapTiles.DoorUp:
                            DoorsAndStairs doorUp = new DoorsAndStairs(spawnPointXY, BlockType.DoorUp);
                            break;
                        case MapTiles.DoorDown:
                            DoorsAndStairs doorDown = new DoorsAndStairs(spawnPointXY, BlockType.DoorDown);
                            break;
                        case MapTiles.DoorRight:
                            DoorsAndStairs doorRight = new DoorsAndStairs(spawnPointXY, BlockType.DoorRight);
                            break;
                        case MapTiles.DoorLeft:
                            DoorsAndStairs doorLeft = new DoorsAndStairs(spawnPointXY, BlockType.DoorLeft);
                            break;
                        case MapTiles.DoorLockedUp:
                            DoorsAndStairs doorLockedUp = new DoorsAndStairs(spawnPointXY, BlockType.DoorLockedUp);
                            break;
                        case MapTiles.DoorLockedLeft:
                            DoorsAndStairs doorLockedLeft = new DoorsAndStairs(spawnPointXY, BlockType.DoorLockedLeft);
                            break;
                        case MapTiles.DoorLockedDown:
                            DoorsAndStairs doorLockedDown = new DoorsAndStairs(spawnPointXY, BlockType.DoorLockedDown);
                            break;
                        case MapTiles.DoorLockedRight:
                            DoorsAndStairs doorLockedRight = new DoorsAndStairs(spawnPointXY, BlockType.DoorLockedRight);
                            break;
                        case MapTiles.DoorSpecialLeft2_1:
                            DoorsAndStairs doorLeft2_1 = new DoorsAndStairs(spawnPointXY, BlockType.DoorSpecialLeft2_1);
                            break;
                        case MapTiles.DoorSpecialRight3_1:
                            DoorsAndStairs doorRight3_1 = new DoorsAndStairs(spawnPointXY, BlockType.DoorSpecialRight3_1);
                            break;
                        case MapTiles.DoorSpecialUp1_1:
                            DoorsAndStairs doorUp1_1 = new DoorsAndStairs(spawnPointXY, BlockType.DoorSpecialUp1_1);
                            break;
                    }
                }
            }
        }

        private void InitializeAllEnemies()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if(_tiles[i][j] == (int)MapTiles.SpawnEnemy)
                    {
                        int _spawnCoordX = i * 16;
                        int _spawnCoordY = j * 16;
                        Point spawnPointXY = new Point(_spawnCoordX, _spawnCoordY);
                        switch (_enemyType)
                        { 
                            case EnemyType.Gel:
                                _enemyStorage.Add(new Gel(spawnPointXY));
                                break;
                            case EnemyType.Goriya:
                                _enemyStorage.Add(new Goriya(spawnPointXY));
                                break;
                            case EnemyType.Keese:
                                _enemyStorage.Add(new Keese(spawnPointXY));
                                break;
                            case EnemyType.OldMan:
                                _enemyStorage.Add(new OldMan(spawnPointXY));
                                break;
                            case EnemyType.Stalfos:
                                _enemyStorage.Add(new Stalfos(spawnPointXY));
                                break;
                            case EnemyType.Trap:
                                _enemyStorage.Add(new Trap(spawnPointXY));
                                break;
                            case EnemyType.WallMaster:
                                _enemyStorage.Add(new WallMaster(spawnPointXY));
                                break;
                        }
                    }
                }
            }
        }
    }
}