using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class Key : Item
    {
        private bool _activated;
        private readonly IRoom _room;

        public Key(Point location, IRoom room) : base(location)
        {
            _room = room;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateKey();

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (!_activated) return NoOp.Instance;

            Used = true;
            SoundEffectManager.Instance.PlayPickupDroppedHeartKey();
            return new AddKey(player);
        }

        public override void Update()
        {
            Sprite?.Update();

            if (_activated || _room.SomeEnemiesAlive) return;
            _activated = true;
            SoundEffectManager.Instance.PlayKeyAppear();
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
