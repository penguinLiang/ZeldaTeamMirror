using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Items;

// ReSharper disable SwitchStatementMissingSomeCases (handled at runtime)
namespace Zelda.Dungeon
{
    public class Room : IRoom
    {
        private const int TileWidthHeight = 16;

        public List<IEnemy> Enemies { get; } = new List<IEnemy>();
        public bool SomeEnemiesAlive => Enemies.Any(enemy => enemy.Alive);
        public List<ICollideable> Collidables { get; } = new List<ICollideable>();
        public List<IDrawable> Drawables { get; } = new List<IDrawable>();
        public List<IItem> Items { get; } = new List<IItem>();
        public List<ITransitionResetable> TransitionResetables { get; } = new List<ITransitionResetable>();
        public Dictionary<Direction, DoorBase> Doors { get; } = new Dictionary<Direction, DoorBase>();

        private readonly EnemyType _enemyType;
        private readonly DungeonManager _dungeonManager;

        // ReSharper disable once SuggestBaseTypeForParameter (the input must be a jagged int array)
        public Room(DungeonManager dungeon, int[][] tiles, int enemyID)
        {
            _enemyType = (EnemyType) enemyID;
            _dungeonManager = dungeon;
            Func<MapTile, Point, bool>[] possibleBlocks =
            {
                TryAddBarrier,
                TryAddProjectilPassthroughBarrier,
                TryAddNormalDoor,
                TryAddLockedDoor,
                TryAddSpecialDoor,
                TryAddBombableWall,
                TryAddStair,
                TryAddNonStandardTiles
            };
            
            for (var row = 0; row < tiles.Length; row++)
            {
                for (var col = 0; col < tiles[row].Length; col++)
                {
                    var location = new Point(col * TileWidthHeight, row * TileWidthHeight);
                    var tile = (MapTile) tiles[row][col];
                    var success = false;
                    foreach (var possibleBlock in possibleBlocks)
                    {
                        success = possibleBlock(tile, location);
                        if (success) break;
                    }
                    if (!success) throw new ArgumentOutOfRangeException(tile.ToString());
                }
            }
        }

        private bool TryAddNonStandardTiles(MapTile tile, Point location) {
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
                    Enemies.Add(EnemyFactory.MakeEnemy(location, _enemyType));
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

        private bool TryAddNormalDoor(MapTile tile, Point location)
        {
            BlockType blockType;
            Direction direction;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (tile)
            {
                case MapTile.DoorRight:
                    blockType = BlockType.DoorRight;
                    direction = Direction.Right;
                    break;
                case MapTile.DoorLeft:
                    blockType = BlockType.DoorLeft;
                    direction = Direction.Left;
                    break;
                case MapTile.DoorUp:
                    blockType = BlockType.DoorUp;
                    direction = Direction.Up;
                    break;
                case MapTile.DoorDown:
                    blockType = BlockType.DoorDown;
                    direction = Direction.Down;
                    break;
                default:
                    return false;
            }

            var door = new NormalDoor(_dungeonManager, location, blockType);
            Doors.Add(direction, door);
            Drawables.Add(door);
            Collidables.Add(door);
            return true;
        }

        private bool TryAddLockedDoor(MapTile tile, Point location)
        {
            BlockType blockType;
            Direction direction;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (tile)
            {
                case MapTile.DoorLockedRight:
                    blockType = BlockType.DoorLockedRight;
                    direction = Direction.Right;
                    break;
                case MapTile.DoorLockedLeft:
                    blockType = BlockType.DoorLockedLeft;
                    direction = Direction.Left;
                    break;
                case MapTile.DoorLockedUp:
                    blockType = BlockType.DoorLockedUp;
                    direction = Direction.Up;
                    break;
                case MapTile.DoorLockedDown:
                    blockType = BlockType.DoorLockedDown;
                    direction = Direction.Down;
                    break;
                default:
                    return false;
            }

            var door = new LockedDoor(_dungeonManager, location, blockType);
            Doors.Add(direction, door);
            Drawables.Add(door);
            Collidables.Add(door);
            return true;
        }

        private bool TryAddSpecialDoor(MapTile tile, Point location)
        {
            DoorBase door;
            Direction direction;
            switch (tile)
            {
                case MapTile.DoorSpecialLeft2_1:
                    door = new DoorSpecialLeft2_1(_dungeonManager, location);
                    direction = Direction.Left;
                    break;
                case MapTile.DoorSpecialRight3_1:
                    door = new DoorSpecialRight3_1(_dungeonManager, location);
                    direction = Direction.Right;
                    break;
                default:
                    return false;
            }
            Doors.Add(direction, door);
            Drawables.Add(door);
            Collidables.Add(door);
            return true;
        }

        private bool TryAddBombableWall(MapTile tile, Point location)
        {
            BlockType blockType;
            Direction direction;
            switch (tile)
            {
                case MapTile.DoorBombableLeft:
                    blockType = BlockType.BombableWallLeft;
                    direction = Direction.Right;
                    break;
                case MapTile.DoorBombableRight:
                    blockType = BlockType.BombableWallRight;
                    direction = Direction.Left;
                    break;
                case MapTile.DoorBombableUp:
                    blockType = BlockType.BombableWallTop;
                    direction = Direction.Up;
                    break;
                case MapTile.DoorBombableDown:
                    blockType = BlockType.BombableWallBottom;
                    direction = Direction.Down;
                    break;
                default:
                    return false;
            }

            var door = new BombDoor(_dungeonManager, location, blockType);
            Doors.Add(direction, door);
            Drawables.Add(door);
            Collidables.Add(door);
            return true;
        }

        private bool TryAddStair(MapTile tile, Point location)
        {
            BlockType blockType;

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

        private bool TryAddProjectilPassthroughBarrier(MapTile tile, Point location)
        {
            BlockType blockType;
            switch (tile)
            {
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
                default:
                    return false;
            }

            var barrier = new ProjectilePassthroughBarrier(location, blockType);
            Collidables.Add(barrier);
            Drawables.Add(barrier);

            return true;
        }

        private bool TryAddBarrier(MapTile tile, Point location)
        {
            BlockType blockType;
            switch (tile)
            {
                case MapTile.InvisibleWall:
                    blockType = BlockType.InvisibleBlock;
                    break;
                case MapTile.BlackBarrier:
                    blockType = BlockType.BlackBarrier;
                    break;
                case MapTile.InvisibleProjectileBarrier:
                    blockType = BlockType.InvisibleProjectileBarrier;
                    break;
                default:
                    return false;
            }

            var barrier = new Barrier(location, blockType);
            Collidables.Add(barrier);
            Drawables.Add(barrier);

            return true;
        }

        public void TransitionReset()
        {
            foreach (var transitionResetable in TransitionResetables)
            {
                transitionResetable.Reset();
            }
        }
    }
}