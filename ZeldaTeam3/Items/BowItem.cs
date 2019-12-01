using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BowItem : Item
    {
        private readonly Secondary _bowLevel;

        protected override ISprite Sprite { get; }
        private int _price;
        public BowItem(Point location, Secondary bowLevel, int price = 0) : base(location, price)
        {
            _price = price;
            _bowLevel = bowLevel;
            Sprite = bowLevel == Secondary.Bow ? ItemSpriteFactory.Instance.CreateBow()
                : ItemSpriteFactory.Instance.CreateFireBow();
        }
        
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;

            if(_price>0)
            { 
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.Bow);
                }
                    Used = false;
                    return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new AddSecondaryItem(player, Secondary.Bow);
        }
    }
}
