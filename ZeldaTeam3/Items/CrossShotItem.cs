using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class CrossShotItem : Item
    {

        private int _price; 
        public CrossShotItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
            if(_price>0){
                if((player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None)&& player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new LinkSecondaryAssign(player, Secondary.LaserBeam);
                }
                else{
                    return new NoOp();
                    }
            }
            else {
            Used = false;
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAssign(player, Secondary.LaserBeam);
                }
        }

    }
}
