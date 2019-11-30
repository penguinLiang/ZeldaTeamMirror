using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class SilverArrowItem : Item
    {
        private int _price;
        public SilverArrowItem(Point location,int price = 0) : base(location, price)
        {
           _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(price)){
                    SoundEffectManager.PlayPickupItem();
                    player.Inventory.AddSecondaryItem(Secondary.SilverArrow);
                }
                else{
                    Used = false;
                }
            }
            SoundEffectManager.Instance.PlayPickupItem();
            return new NoOp();
        }

    }
}
