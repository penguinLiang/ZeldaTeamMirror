using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BaitItem : Item
    {

        public int _price;
        private readonly FrameDelay _delay = new FrameDelay(90);
        public BaitItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite
        
        public override ICommand PlayerEffect(IPlayer player)
        {
           _delay.Update();
            Used = false;
            if(!_delay.Delayed && (_price>0 && (player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None))) 
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new LinkSecondaryAssign(player, Secondary.Bait);
                }
                return new NoOp();
            }
            else if(_price == 0)
            {
                Used = true;
                SoundEffectManager.Instance.PlayPickupItem();
                return new LinkSecondaryAssign(player, Secondary.Bait);
            }
            else 
                return new NoOp();
        }
    }
}
