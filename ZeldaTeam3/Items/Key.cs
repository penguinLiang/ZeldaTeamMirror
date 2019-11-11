using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;
using Zelda.Dungeon;

namespace Zelda.Items
{
    internal class Key : Item
    {
        private bool _activated;
        private Room _room;

        public Key(Point location, Room room) : base(location)
        {
            _room = room;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateKey();

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (_activated)
            {
                Used = true;
                SoundEffectManager.Instance.PlayPickupDroppedHeartKey();
                return new AddKey(player);
            }
            return NoOp.Instance;
        }

        public override void Update()
        {
            Sprite?.Update();
            if (!_activated && !_room.SomeEnemiesAlive)
            {
                _activated = true;
                SoundEffects.SoundEffectManager.Instance.PlayKeyAppear();
            }
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
