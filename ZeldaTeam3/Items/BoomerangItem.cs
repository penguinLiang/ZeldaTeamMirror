﻿using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BoomerangItem : Item
    {
        private bool _activated;
        private readonly Room _room;
        private int _price;

        public BoomerangItem(Point location, Room room, int price = 0) : base(location, price)
        {
            _room = room;
            _price = price;
        }


        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWoodBoomerang();

        public override ICommand PlayerEffect(IPlayer player)
        {
            if (!_activated) return NoOp.Instance;
            
            Used = true;

            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new AddSecondaryItem(player, Secondary.Boomerang);
                }
                Used = false;
                return new NoOp();
            }
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
