using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;
using Zelda.Dungeon;

namespace Zelda.Items
{
    internal class BoomerangItem : Item
    {
        private bool _activated;
        private Room _room;

        public BoomerangItem(Point location, Room room) : base(location)
        {
            _room = room;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWoodBoomerang();

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (!_activated) return NoOp.Instance;

            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.Boomerang);
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
