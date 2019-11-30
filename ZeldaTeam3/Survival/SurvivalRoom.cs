using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;

// ReSharper disable SwitchStatementMissingSomeCases (handled at runtime)
namespace Zelda.Survival
{
    public sealed class SurvivalRoom : IRoom
    {
        private const int TileWidthHeight = 16;

        public List<IEnemy> Enemies { get; } = new List<IEnemy>();
        public bool SomeEnemiesAlive => Enemies.Any(enemy => enemy.Alive);
        public List<ICollideable> Collidables { get; } = new List<ICollideable>();
        public List<IDrawable> Drawables { get; } = new List<IDrawable>();
        public List<IItem> Items { get; } = new List<IItem>();
        public List<ITransitionResetable> TransitionResetables { get; } = new List<ITransitionResetable>();
        public Dictionary<Direction, DoorBase> Doors { get; } = new Dictionary<Direction, DoorBase>();
        public List<IItem> ShopItems { get; } = new List<IItem>();
        public List<Point> SpawnTiles { get; } = new List<Point>();

        private readonly Random _rnd = new Random((int)DateTime.Now.Ticks);

        private readonly IDungeonManager _survivalManager;
        private readonly ShopManager _shopManager;

        // ReSharper disable once SuggestBaseTypeForParameter (the input must be a jagged int array)
        public SurvivalRoom(IDungeonManager manager, int[][] tiles)
        {
            _survivalManager = manager;

            Func<MapTile, Point, bool>[] possibleBlocks =
            {
                TryAddBarrier,
                TryAddProjectilPassthroughBarrier,
                TryAddNormalDoor,
                TryAddNonStandardTiles,
                TryAddShopTiles
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

            var barrier = new ProjectilPassthroughBarrier(location, blockType);
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
                case MapTile.ProjectileBlackBarrier:
                    blockType = BlockType.ProjectileBlackBarrier;
                    break;
                default:
                    return false;
            }

            var barrier = new Barrier(location, blockType);
            Collidables.Add(barrier);
            Drawables.Add(barrier);

            return true;
        }

        private bool TryAddNormalDoor(MapTile tile, Point location)
        {
            BlockType blockType;
            Direction direction;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (tile)
            {
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

            var door = new NormalDoor(_survivalManager, location, blockType);
            Doors.Add(direction, door);
            Drawables.Add(door);
            Collidables.Add(door);
            return true;
        }

        public void SpawnEnemy(int enemyId)
        {
            //TODO: To be called by WaveManager or something. Should only spawn on the tiles within the current zone.
            //Update to control the zones that the player has unlocked.
            //currently we have an array of all the spawn tiles, this can spawn all the waves.
            if (SpawnTiles.Count == 0) return;

            var enemy = EnemyFactory.MakeEnemy(SpawnTiles[_rnd.Next(SpawnTiles.Count)], (EnemyType)enemyId);
            Enemies.Add(enemy);
            enemy.Spawn();
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
                case MapTile.PushableBlock:
                    var pushableBlock = new MovableBlock(location);
                    Collidables.Add(pushableBlock);
                    Drawables.Add(pushableBlock);
                    TransitionResetables.Add(pushableBlock);
                    break;
                case MapTile.SpawnEnemy:
                    SpawnTiles.Add(location);
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
                case MapTile.Room2_1Block:
                    break;
                default:
                    return false;
            }

            return true;
        }

        private bool TryAddShopTiles(MapTile tile, Point location)
        {
            switch(tile)
            {
                case MapTile.AlchemyCoin:
                    ShopItems.Add(new AlchemyCoinItem(location));
                    break;
                case MapTile.Arrow:
                    ShopItems.Add(new ArrowItem(location, Secondary.Arrow));
                    break;
                case MapTile.ATWBoomerang:
                    ShopItems.Add(new ATWBoomerangItem(location));
                    break;
                case MapTile.Bait:
                    ShopItems.Add(new BaitItem(location));
                    break;
                case MapTile.Bomb:
                    ShopItems.Add(new BombItem(location));
                    break;
                case MapTile.BombLauncher:
                    ShopItems.Add(new BombLauncherItem(location));
                    break;
                case MapTile.BombUpgrade:
                    ShopItems.Add(new BombUpgradeItem(location));
                    break;
                case MapTile.Boomerang:
                    ShopItems.Add(new BoomerangItem(location, this));
                    break;
                case MapTile.Bow:
                    ShopItems.Add(new BowItem(location, Secondary.Bow));
                    break;
                case MapTile.Clock:
                    ShopItems.Add(new ClockItem(location));
                    break;
                case MapTile.CrossShot:
                    ShopItems.Add(new CrossShotItem(location));
                    break;
                case MapTile.KeyBarrier:
                    Collidables.Add(new KeyBarrier(_shopManager, location, BlockType.KeyBarrier));
                    break;
                case MapTile.MagicSword:
                    ShopItems.Add(new MagicSwordItem(location));
                    break;
                case MapTile.RupeeUpgrade:
                    ShopItems.Add(new RupeeUpgradeItem(location));
                    break;
                case MapTile.SilverArrow:
                    ShopItems.Add(new SilverArrowItem(location));
                    break;
                case MapTile.SpawnShopKeep:
                    Enemies.Add(new OldMan(location));
                    break;
                case MapTile.Star:
                    ShopItems.Add(new StarItem(location));
                    break;
                case MapTile.WalletUpgrade:
                    ShopItems.Add(new WalletUpgradeItem(location));
                    break;
                case MapTile.WhiteSword:
                    ShopItems.Add(new WhiteSwordItem(location));
                    break;
                case MapTile.FireBow:
                    ShopItems.Add(new FireBowItem(location));
                    break;
                case MapTile.Fairy:
                    ShopItems.Add(new Fairy(location));
                    break;
                default:
                    return false;
            }
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