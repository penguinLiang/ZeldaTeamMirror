using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class FireBowItem : Item
    {

        private int _price;
        public FireBowItem(Point location,int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
           
            Used = true;

            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.FireBow);
                }
                else {
                    Used = false;
                    return new NoOp();
                }
            }else{
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.FireBow);
                }
        }

        public ICommand BuyItem()
        {
            //In the actual thing this will be triggered if Link has enough to buy it
            //Item added to inventory, collision cleared and sprite hidden
            //potentially monitored in ShopManager?
            //Upgrade to Bow
            return new NoOp();
        }
    }
}
