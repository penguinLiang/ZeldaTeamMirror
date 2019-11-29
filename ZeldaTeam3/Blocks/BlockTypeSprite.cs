using Zelda.ShaderEffects;

namespace Zelda.Blocks
{
    internal class BlockTypeSprite
    {
        public static ISprite Sprite(BlockType block)
        {
            switch (block)
            {
                case BlockType.BombableWallBottom:
                    return BlockSpriteFactory.Instance.CreateBottomWallHole();
                case BlockType.BombableWallLeft:
                    return BlockSpriteFactory.Instance.CreateLeftWallHole();
                case BlockType.BombableWallRight:
                    return BlockSpriteFactory.Instance.CreateRightWallHole();
                case BlockType.BombableWallTop:
                    return BlockSpriteFactory.Instance.CreateTopWallHole();
                case BlockType.DoorLockedDown:
                    return BlockSpriteFactory.Instance.CreateBottomLockedDoor();
                case BlockType.DoorLockedLeft:
                    return BlockSpriteFactory.Instance.CreateLeftLockedDoor();
                case BlockType.DoorLockedRight:
                    return BlockSpriteFactory.Instance.CreateRightLockedDoor();
                case BlockType.DoorLockedUp:
                    return BlockSpriteFactory.Instance.CreateTopLockedDoor();
                case BlockType.DragonStatue:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateStatue2(), true);
                case BlockType.FishStatue:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateStatue1(), true);
                case BlockType.Fire:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateFire(), false);
                case BlockType.ImmovableBlock:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateSolidBlock(), true);
                case BlockType.Water:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateWater(), false);
                case BlockType.Sand:
                    return BlockSpriteFactory.Instance.CreateSand();
                case BlockType.DoorUp:
                    return BlockSpriteFactory.Instance.CreateTopOpenDoor();
                case BlockType.DoorDown:
                    return BlockSpriteFactory.Instance.CreateBottomOpenDoor();
                case BlockType.DoorRight:
                    return BlockSpriteFactory.Instance.CreateRightOpenDoor();
                case BlockType.DoorLeft:
                    return BlockSpriteFactory.Instance.CreateLeftOpenDoor();
                case BlockType.DoorSpecialLeft2_1:
                    return BlockSpriteFactory.Instance.CreateLeftBlockedDoor();
                case BlockType.DoorSpecialRight3_1:
                    return BlockSpriteFactory.Instance.CreateRightBlockedDoor();
                case BlockType.StairSpecialUp1_1:
                    return null;
                case BlockType.DungeonStair:
                    return BlockSpriteFactory.Instance.CreateStairs1();
                case BlockType.BasementStair:
                    return BlockSpriteFactory.Instance.CreateStairs2();
                case BlockType.Block2_1:
                case BlockType.PushableBlock:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateSolidBlock(), true);
                case BlockType.BlackBarrier:
                case BlockType.ProjectileBlackBarrier:
                    return BlockSpriteFactory.Instance.CreateBlackTile();
                case BlockType.InvisibleBlock:
                    return new AlphaPassMask(16, 16);
                case BlockType.RupeeBarrier:
                case BlockType.RupeeBarrierCenter:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateRupeeBarricade(), true);
                case BlockType.KeyBarrier:
                case BlockType.KeyBarrierCenter:
                    return new AlphaPassMask(BlockSpriteFactory.Instance.CreateKeyBarricade(), true);
                default:
                    return null;
            }
        }
    }
}