using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.ShaderEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class NormalDoor : DoorBase
    {
        protected override ISprite Sprite { get; }
        protected override Point Location { get; }
        protected override Point Size { get; }
        protected override Point DrawOffset { get; }
        protected override Rectangle NoOpArea { get; }
        protected override Rectangle TransitionArea { get; }
        protected override ICommand TransitionEffect { get; }

        public NormalDoor(DungeonManager dungeon, Point location, BlockType block)
        {
            Location = location;
            DrawOffset = Point.Zero;

            switch (block)
            {
                case BlockType.DoorRight:
                    Size = new Point(32, 48);
                    NoOpArea = new Rectangle( 0,  16, 16, 16);
                    TransitionArea = new Rectangle( 16,  16, 16, 16);
                    TransitionEffect = new Transition(dungeon, Direction.Right);
                    DrawOffset = new Point(0, 8);
                    break;
                case BlockType.DoorLeft:
                    Size = new Point(32, 48);
                    NoOpArea = new Rectangle( 16,  16, 16, 16);
                    TransitionArea = new Rectangle( 0,  16, 16, 16);
                    TransitionEffect = new Transition(dungeon, Direction.Left);
                    DrawOffset = new Point(0, 8);
                    break;
                case BlockType.DoorUp:
                    Size = new Point(32, 32);
                    NoOpArea = new Rectangle(8, 24, 16, 8);
                    TransitionArea = new Rectangle(8, 0, 16, 24);
                    TransitionEffect = new Transition(dungeon, Direction.Up);
                    break;
                case BlockType.DoorDown:
                    Size = new Point(32, 32);
                    NoOpArea = new Rectangle(8, 0, 16, 16);
                    TransitionArea = new Rectangle(8, 16, 16, 16);
                    TransitionEffect = new Transition(dungeon, Direction.Down);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(block.ToString());
            }

            Sprite = new AlphaPassMask(BlockTypeSprite.Sprite(block), true);
        }
    }
}
