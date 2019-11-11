using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;
using Zelda.Dungeon;

namespace Zelda.Items
{
    internal class HeartContainer : Item
    {
        private bool _activated;
        private Room _room;

        public HeartContainer(Point location, Room room) : base(location)
        {
            _room = room;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateHeartContainer();
        protected override Point Size { get; } = new Point(16, 16);
        protected override Point Offset { get; } = Point.Zero;
        protected override Point DrawOffset { get; } = Point.Zero;

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (_activated)
            {
                Used = true;
                SoundEffectManager.Instance.PlayPickupDroppedHeartKey();
                return new LinkAddHeart(player);
            }
            return NoOp.Instance;
        }

        public override void Update()
        {
            Sprite?.Update();
            if (!_activated && !_room.SomeEnemiesAlive)
                _activated = true;
        }

        public override void Draw()
        {
            if (_activated && !Used) Sprite?.Draw((Location + DrawOffset).ToVector2());
        }

        public override void Reset()
        {
            Used = false;
            _activated = false;
        }
    }
}
