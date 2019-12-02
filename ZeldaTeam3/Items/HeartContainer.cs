using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class HeartContainer : Item
    {
        private bool _activated;
        private readonly IRoom _room;

        public HeartContainer(Point location, IRoom room) : base(location)
        {
            _room = room;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateHeartContainer();
        protected override Point Size { get; } = new Point(16, 16);
        protected override Point Offset { get; } = Point.Zero;
        protected override Point DrawOffset { get; } = Point.Zero;

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (!_activated) return NoOp.Instance;

            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkAddHeart(player);
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
