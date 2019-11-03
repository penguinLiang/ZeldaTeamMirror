using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Dungeon;
using Zelda.Projectiles;

namespace Zelda.Dungeon
{
    public class Room
    {
        private const int TileWidthHeight = 16;

        public IList<IEnemy> Enemies = new List<IEnemy>();
        public IList<ICollideable> Collidables = new List<ICollideable>();
        public IList<IDrawable> Drawables = new List<IDrawable>();

        public ProjectileManager _projectileManager = new ProjectileManager();
        public IList<IProjectile> Projectiles;

        private readonly EnemyType _enemyType;
        private DungeonManager _dungeonManager;

        // ReSharper disable once SuggestBaseTypeForParameter (the input must be a jagged int array)
        public Room(DungeonManager dungeon, int[][] tiles, int enemyID)
        {
            _enemyType = (EnemyType) enemyID;
            _dungeonManager = dungeon;
            Projectiles = _projectileManager.Projectiles;

            for (var row = 0; row < tiles.Length; row++)
            {
                for (var col = 0; col < tiles[row].Length; col++)
                {
                    var location = new Point(col * TileWidthHeight, row * TileWidthHeight);
                    var tile = (MapTile) tiles[row][col];

                    if (!TryAddBarrier(tile, location) && !TryAddLeftRightDoor(tile, location) 
                        && !TryAddUpDownDoor(tile, location) && !TryAddStair(tile, location) &&
                        !TryAddNonStandardTiles(tile, location))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private IEnemy MakeEnemy(Point spawnPoint)
        {
            switch (_enemyType)
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
                case EnemyType.None:
                    throw new Exception("Room spawns an enemy but no type is set");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool TryAddNonStandardTiles(MapTile tile, Point location) {
            // ReSharper disable once SwitchStatementMissingSomeCases (cases are covered elsewhere)
            switch (tile)
            {
                case MapTile.Key:
                    var key = new Key(location);
                    Collidables.Add(key);
                    Drawables.Add(key);
                    break;
                case MapTile.Compass:
                    var compass = new Compass(location);
                    Collidables.Add(compass);
                    Drawables.Add(compass);
                    break;
                case MapTile.Map:
                    var map = new Map(location);
                    Collidables.Add(map);
                    Drawables.Add(map);
                    break;
                case MapTile.Bow:
                    var bow = new BowItem(location);
                    Collidables.Add(bow);
                    Drawables.Add(bow);
                    break;
                case MapTile.Triforce:
                    var triforce = new Triforce(location);
                    Collidables.Add(triforce);
                    Drawables.Add(triforce);
                    break;
                case MapTile.Room2_1Block:
                    var room21Block = new MovableBlock(location);
                    Collidables.Add(room21Block);
                    Drawables.Add(room21Block);
                    break;
                case MapTile.PushableBlock:
                    var pushableBlock = new MovableBlock(location);
                    Collidables.Add(pushableBlock);
                    Drawables.Add(pushableBlock);
                    break;
                case MapTile.SpawnEnemy:
                    Enemies.Add(MakeEnemy(location));
                    break;
                case MapTile.Sand:
                    break;
                case MapTile.Heart:
                    var heart = new HeartContainer(location);
                    Collidables.Add(heart);
                    Drawables.Add(heart);
                    break;
                case MapTile.Boomerang:
                    var boomerang = new BoomerangItem(location);
                    Collidables.Add(boomerang);
                    Drawables.Add(boomerang);
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

        private bool TryAddLeftRightDoor(MapTile tile, Point location)
        {
            BlockType blockType;

            // ReSharper disable once SwitchStatementMissingSomeCases (cases are covered elsewhere)
            switch (tile)
            {
                case MapTile.DoorRight:
                    blockType = BlockType.DoorRight;
                    break;
                case MapTile.DoorLeft:
                    blockType = BlockType.DoorLeft;
                    break;
                case MapTile.DoorLockedLeft:
                    blockType = BlockType.DoorLockedLeft;
                    break;
                case MapTile.DoorLockedRight:
                    blockType = BlockType.DoorLockedRight;
                    break;
                case MapTile.DoorSpecialLeft2_1:
                    blockType = BlockType.DoorSpecialLeft2_1;
                    break;
                case MapTile.DoorSpecialRight3_1:
                    blockType = BlockType.DoorSpecialRight3_1;
                    break;
                case MapTile.DoorBombableLeft:
                    blockType = BlockType.BombableWallLeft;
                    break;
                case MapTile.DoorBombableRight:
                    blockType = BlockType.BombableWallRight;
                    break;
                default:
                    return false;
            }

            var leftRightDoors = new LeftRightDoor(_dungeonManager, location, blockType);
            Collidables.Add(leftRightDoors);
            Drawables.Add(leftRightDoors);

            return true;
        }

        private bool TryAddUpDownDoor(MapTile tile, Point location)
        {
            BlockType blockType;

            // ReSharper disable once SwitchStatementMissingSomeCases (cases are covered elsewhere)
            switch (tile)
            {
                case MapTile.DoorUp:
                    blockType = BlockType.DoorUp;
                    break;
                case MapTile.DoorDown:
                    blockType = BlockType.DoorDown;
                    break;
                case MapTile.DoorLockedUp:
                    blockType = BlockType.DoorLockedUp;
                    break;
                case MapTile.DoorLockedDown:
                    blockType = BlockType.DoorLockedDown;
                    break;
                case MapTile.DoorBombableUp:
                    blockType = BlockType.BombableWallTop;
                    break;
                case MapTile.DoorBombableDown:
                    blockType = BlockType.BombableWallBottom;
                    break;
                default:
                    return false;
            }

            var upDownDoors = new UpDownDoor(_dungeonManager, location, blockType);
            Collidables.Add(upDownDoors);
            Drawables.Add(upDownDoors);

            return true;
        }
        private bool TryAddStair(MapTile tile, Point location)
        {
            BlockType blockType;

            // ReSharper disable once SwitchStatementMissingSomeCases (Handled in other cases)
            switch (tile)
            {
                case MapTile.DungeonStairs:
                    blockType = BlockType.DungeonStair;
                    break;
                case MapTile.BasementStairs:
                    blockType = BlockType.BasementStair;
                    break;
                case MapTile.StairSpecialUp1_1:
                    blockType = BlockType.StairSpecialUp1_1;
                    break;
                default:
                    return false;
            }

            var stairs = new Stair(_dungeonManager, location, blockType);
            Collidables.Add(stairs);
            Drawables.Add(stairs);

            return true;
        }

        private bool TryAddBarrier(MapTile tile, Point location)
        {
            BlockType blockType;
            // ReSharper disable once SwitchStatementMissingSomeCases (cases are covered elsewhere)
            switch (tile)
            {
                case MapTile.InvisibleWall:
                    blockType = BlockType.InvisibleBlock;
                    break;
                case MapTile.Fire:
                    blockType = BlockType.Fire;
                    break;
                case MapTile.Water:
                    blockType = BlockType.Water;
                    break;
                case MapTile.FishStatue:
                    blockType = BlockType.FishStatue;
                    break;
                case MapTile.DragonStatue:
                    blockType = BlockType.DragonStatue;
                    break;
                case MapTile.ImmovableBlock:
                    blockType = BlockType.ImmovableBlock;
                    break;
                case MapTile.BlackBarrier:
                    blockType = BlockType.BlackBarrier;
                    break;
                default:
                    return false;
            }

            var barrier = new Barrier(location, blockType);
            Collidables.Add(barrier);
            Drawables.Add(barrier);

            return true;
        }
    }
}