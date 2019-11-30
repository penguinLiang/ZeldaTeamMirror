using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class StarItem : Item
    {
        private int _price;
        public StarItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
            //If link collides with, and he has enough money, Buy Item, else leave the item there
            //Link also needs enough space in inventory, or to already have one of this item in inventory
            //Added to EXTRA SLOT 1 || 2
            Used = true;
            if(_price>0){
                if(player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None){
            
                if(player.Inventory.TryRemoveRupee(_price)){
                    //add to inventory
                    player.Inventory.AssignSecondaryItem(Secondary.Star);
                    SoundEffectManager.Instance.PlayPickupItem();
                    }
                }
                Used = false;
                return new NoOp();
            }
            else {
            SoundEffectManager.Instance.PlayPickupItem();
            return new NoOp();}
        }

        public ICommand BuyItem()
        {
            //In the actual thing this will be triggered if Link has enough to buy it
            //Item added to inventory, collision cleared and sprite hidden
            //potentially monitored in ShopManager?
            return new NoOp();
        }
    }
}
