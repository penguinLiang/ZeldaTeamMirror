using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BaitItem : Item
    {

        public int _price;
        public BaitItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite
        
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = false;
            if(_price>0 && (player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None)) 
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new LinkSecondaryAssign(player, Secondary.Bait);
                }
                return new NoOp();
            }

            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAssign(player, Secondary.Bait);
        }
    }
}
