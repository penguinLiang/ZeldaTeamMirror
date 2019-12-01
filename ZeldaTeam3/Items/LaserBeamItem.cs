using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class LaserBeamItem : Item
    {

        private int _price; 
        public LaserBeamItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }
        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateLaserBeam();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = false;
            if(_price>0)
            {
                if((player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None) && player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new LinkSecondaryAssign(player, Secondary.LaserBeam);
                }
                    return new NoOp();
            }
            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAssign(player, Secondary.LaserBeam);
        }

    }
}
