using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.Projectiles;

namespace Zelda.Blocks
{
    internal class BombDoor : NormalDoor
    {
        private readonly BlockType _block;
        protected override ISprite Sprite => _sprite;
        protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unblocked;

        private static BlockType UnblockedType(BlockType block)
        {
            switch (block)
            {
                case BlockType.BombableWallRight:
                    return BlockType.DoorRight;
                case BlockType.BombableWallLeft:
                    return BlockType.DoorLeft;
                case BlockType.BombableWallTop:
                    return BlockType.DoorUp;
                case BlockType.BombableWallBottom:
                    return BlockType.DoorDown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public BombDoor(DungeonManager dungeon, Point location, BlockType block) : base(dungeon, location, UnblockedType(block))
        {
            _block = block;
            switch (block)
            {
                case BlockType.BombableWallRight:
                    TransitionEffect = new Transition(dungeon, Direction.Right, true);
                    break;
                case BlockType.BombableWallLeft:
                    TransitionEffect = new Transition(dungeon, Direction.Left, true);
                    break;
                case BlockType.BombableWallTop:
                    TransitionEffect = new Transition(dungeon, Direction.Up, true);
                    break;
                case BlockType.BombableWallBottom:
                    TransitionEffect = new Transition(dungeon, Direction.Down, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(block.ToString());
            }
        }

        public override void Reset()
        {
            _sprite = null;
            _unblocked = false;
        }

        public override void Unblock()
        {
            _unblocked = true;
            _sprite = BlockTypeSprite.Sprite(_block);
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (_unblocked) return base.PlayerEffect(player);

            return new MoveableHalt(player);
        }

        public override ICommand ProjectileEffect(IProjectile projectile)
        {
            if (!_unblocked && projectile is Bomb)
            {
                SoundEffects.SoundEffectManager.Instance.PlayPuzzleSolved();
                Unblock();
            }

            return NoOp.Instance;
        }
    }
}
