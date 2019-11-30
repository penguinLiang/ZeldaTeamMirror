using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Items;

// ReSharper disable SwitchStatementMissingSomeCases (handled at runtime)
namespace Zelda.Survival
{
    public class SurvivalRoom : IRoom
    {
        private const int TileWidthHeight = 16;

        // Wave handles enemy status since there is a spawn delay, this is only used for items
        public bool SomeEnemiesAlive => false;
        public List<ICollideable> Collidables { get; } = new List<ICollideable>();
        public List<IDrawable> Drawables { get; } = new List<IDrawable>();
        public List<IItem> Items { get; } = new List<IItem>();
        public List<ITransitionResetable> TransitionResetables { get; } = new List<ITransitionResetable>();
        public Dictionary<Direction, DoorBase> Doors { get; } = new Dictionary<Direction, DoorBase>();
        public List<Point> SpawnTiles { get; } = new List<Point>();
        public List<IItem> BuyableItems { get; } = new List<IItem>();
        public List<IBarricade> Barricade { get; } = new List<IBarricade>();
        //TODO: Change this to be IBuyable
        //TODO: Change all items to use IBuyable

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
                    BuyableItems.Add(new AlchemyCoinItem(location));
                    break;
                case MapTile.Arrow:
                    BuyableItems.Add(new ArrowItem(location, Secondary.Arrow));
                    break;
                case MapTile.ATWBoomerang:
                    BuyableItems.Add(new ATWBoomerangItem(location));
                    break;
                case MapTile.Bait:
                    BuyableItems.Add(new BaitItem(location));
                    break;
                case MapTile.Bomb:
                    BuyableItems.Add(new BombItem(location));
                    break;
                case MapTile.BombLauncher:
                    BuyableItems.Add(new BombLauncherItem(location));
                    break;
                case MapTile.BombUpgrade:
                    BuyableItems.Add(new BombUpgradeItem(location));
                    break;
                case MapTile.Boomerang:
                    BuyableItems.Add(new BoomerangItem(location, this));
                    break;
                case MapTile.Bow:
                    BuyableItems.Add(new BowItem(location, Secondary.Bow));
                    break;
                case MapTile.Clock:
                    BuyableItems.Add(new ClockItem(location));
                    break;
                case MapTile.CrossShot:
                    BuyableItems.Add(new CrossShotItem(location));
                    break;
                case MapTile.KeyBarrier:
                    Barricade.Add(new KeyBarrier(location, BlockType.KeyBarrier));
                    //BuyableItems.Add(new KeyBarrier(_shopManager, location, BlockType.KeyBarrier));
                    break;
                case MapTile.KeyBarrierCenter:
                    Barricade.Add(new KeyBarrierCenter(location, BlockType.KeyBarrierCenter));
                    break;
                case MapTile.RupeeBarrier:
                    Barricade.Add(new RupeeBarrier(location, BlockType.RupeeBarrier));
                    break;
                case MapTile.RupeeBarrierCenter:
                    Barricade.Add(new RupeeBarrierCenter(location, BlockType.RupeeBarrierCenter));
                    break;
                case MapTile.MagicSword:
                    BuyableItems.Add(new MagicSwordItem(location));
                    break;
                case MapTile.RupeeUpgrade:
                    BuyableItems.Add(new RupeeUpgradeItem(location));
                    break;
                case MapTile.SilverArrow:
                    BuyableItems.Add(new SilverArrowItem(location));
                    break;
                case MapTile.SpawnShopKeep:
                    break;
                case MapTile.Star:
                    BuyableItems.Add(new StarItem(location));
                    break;
                case MapTile.WalletUpgrade:
                    BuyableItems.Add(new WalletUpgradeItem(location));
                    break;
                case MapTile.WhiteSword:
                    BuyableItems.Add(new WhiteSwordItem(location));
                    break;
                case MapTile.FireBow:
                    BuyableItems.Add(new FireBowItem(location));
                    break;
                case MapTile.Fairy:
                    BuyableItems.Add(new Fairy(location));
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