using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class DoorSpecialLeft2_1 : NormalDoor
    {
        protected override ISprite Sprite => _sprite;
        protected override ICommand TransitionEffect { get; }
        private ISprite _sprite;
        private bool _unlocked;
        private int _activations;

        public DoorSpecialLeft2_1(DungeonManager dungeon, Point location) : base(dungeon, location, BlockType.DoorLeft)
        {
            TransitionEffect = new Transition(dungeon, Direction.Left, true);
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorSpecialLeft2_1);
        }

        public override void Reset()
        {
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorSpecialLeft2_1);
            _unlocked = false;
        }

        public override void Unblock()
        {
            _unlocked = true;
            _sprite = BlockTypeSprite.Sprite(BlockType.DoorLeft);
        }

        public override void Activate()
        {
            _activations++;
            if (_activations > 1) Unblock();
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return _unlocked ? base.PlayerEffect(player) : new MoveableHalt(player);
        }
    }
}
