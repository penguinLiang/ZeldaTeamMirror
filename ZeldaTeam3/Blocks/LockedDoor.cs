﻿using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class LockedDoor : NormalDoor
    {
        private readonly BlockType _block;
        protected override ISprite Sprite => _sprite;
        protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unlocked;

        private static BlockType UnlockedType(BlockType block)
        {
            switch (block)
            {
                case BlockType.DoorLockedRight:
                    return BlockType.DoorRight;
                case BlockType.DoorLockedLeft:
                    return BlockType.DoorLeft;
                case BlockType.DoorLockedUp:
                    return BlockType.DoorUp;
                case BlockType.DoorLockedDown:
                    return BlockType.DoorDown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public LockedDoor(DungeonManager dungeon, Point location, BlockType block) : base(dungeon, location, UnlockedType(block))
        {
            _block = block;
            switch (block)
            {
                case BlockType.DoorLockedRight:
                    TransitionEffect = new Transition(dungeon, Direction.Right, true);
                    break;
                case BlockType.DoorLockedLeft:
                    TransitionEffect = new Transition(dungeon, Direction.Left, true);
                    break;
                case BlockType.DoorLockedUp:
                    TransitionEffect = new Transition(dungeon, Direction.Up, true);
                    break;
                case BlockType.DoorLockedDown:
                    TransitionEffect = new Transition(dungeon, Direction.Down, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(block.ToString());
            }
            _sprite = BlockTypeSprite.Sprite(_block);
        }

        public override void Reset()
        {
            _sprite = BlockTypeSprite.Sprite(_block);
            _unlocked = false;
        }

        public override void Unblock()
        {
            _unlocked = true;
            _sprite = BlockTypeSprite.Sprite(UnlockedType(_block));
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (_unlocked) return base.PlayerEffect(player);

            if (player.BodyCollision.CollidesWith(LocationOffset(NoOpArea)) && player.Inventory.TryRemoveKey())
            {
                SoundEffects.SoundEffectManager.Instance.PlayDoorUnlock();
                Unblock();
            }

            return new MoveableHalt(player);
        }
    }
}