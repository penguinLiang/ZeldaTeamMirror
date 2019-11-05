using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Enemies;
using Zelda.Items;

namespace Zelda.Dungeon
{
    public class Room
    {
        private const int TileWidthHeight = 16;

        public IList<IEnemy> Enemies = new List<IEnemy>();
        public IList<ICollideable> Collidables = new List<ICollideable>();
        public IList<IDrawable> Drawables = new List<IDrawable>();
        public IList<IItem> Items = new List<IItem>();
        private ActivatableMovableBlock _AMBlock;

        private readonly Random _rnd = new Random();
        private readonly EnemyType _enemyType;
        private readonly DungeonManager _dungeonManager;

        // ReSharper disable once SuggestBaseTypeForParameter (the input must be a jagged int array)
        public Room(DungeonManager dungeon, int[][] tiles, int enemyID)
        {
            _enemyType = (EnemyType) enemyID;
            _dungeonManager = dungeon;

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
                    Items.Add(new Key(location));
                    break;
                case MapTile.Compass:
                    Items.Add(new Compass(location));
                    break;
                case MapTile.Map:
                    Items.Add(new Map(location));
                    break;
                case MapTile.Bow:
                    Items.Add(new BowItem(location));
                    break;
                case MapTile.Triforce:
                    Items.Add(new Triforce(location));
                    break;
                case MapTile.Room2_1Block:
                    var room21Block = new ActivatableMovableBlock(this, BlockType.Block2_1, location);
                    Collidables.Add(room21Block);
                    Drawables.Add(room21Block);
                    _AMBlock = room21Block;
                    break;
                case MapTile.PushableBlock:
                    var pushableBlock = new MovableBlock(this, BlockType.PushableBlock, location);
                    Collidables.Add(pushableBlock);
                    Drawables.Add(pushableBlock);
                    break;
                case MapTile.SpawnEnemy:
                    Enemies.Add(MakeEnemy(location));
                    break;
                case MapTile.Sand:
                    break;
                case MapTile.Heart:
                    Items.Add(new HeartContainer(location));
                    break;
                case MapTile.Boomerang:
                    Items.Add(new BoomerangItem(location));
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

        public void AddDroppedItem(int enemyX, int enemyY)
        {
            int rand = _rnd.Next(100);
            if (rand >= 67) // No drop = 67%
            {
                if (rand < 82)
                {
                    var item = new Rupee(new Point(enemyX + 4, enemyY)); // 1 Rupee = 15%
                    Collidables.Add(item);
                    Drawables.Add(item);
                }
                else if (rand < 92)
                {
                    var item = new DroppedHeart(new Point(enemyX + 4, enemyY + 4)); // Dropped Heart = 10%
                    Collidables.Add(item);
                    Drawables.Add(item);
                }
                else if (rand < 97)
                {
                    var item = new Rupee5(new Point(enemyX + 4, enemyY)); // 5 Rupee = 5%
                    Collidables.Add(item);
                    Drawables.Add(item);
                }
                else if (rand < 99)
                {
                    var item = new BombItem(new Point(enemyX + 4, enemyY)); // Bomb = 2%
                    Collidables.Add(item);
                    Drawables.Add(item);
                }
                else
                {
                    var item = new Fairy(new Point(enemyX + 4, enemyY)); // Fairy = 1%
                    Collidables.Add(item);
                    Drawables.Add(item);
                }
            }
        }

        public void MoveableBlockReset()
        {
            _AMBlock?.Reset();
        }
    }
}