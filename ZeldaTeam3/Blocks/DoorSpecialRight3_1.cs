using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class DoorSpecialRight3_1 : NormalDoor
    {
        protected override ISprite Sprite => _sprite;
        protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unlocked;

        public DoorSpecialRight3_1(DungeonManager dungeon, Point location) : base(dungeon, location, BlockType.DoorRight)
        {
            TransitionEffect = new Transition(dungeon, Direction.Right, true);
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorSpecialRight3_1);
        }

        public override void Reset()
        {
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorSpecialRight3_1);
            _unlocked = false;
        }

        public override void Unblock()
        {
            _unlocked = true;
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorRight);
        }

        public override void Activate()
        {
            Unblock();
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return _unlocked ? base.PlayerEffect(player) : new MoveableHalt(player);
        }
    }
}
