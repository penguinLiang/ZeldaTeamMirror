using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class RupeeUpgradeItem : Item
    {

        public int price { get; private set; }
        public RupeeUpgradeItem(Point location) : base(location)
        {
            price = 20;
            //TODO fix the prices later
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateRupeeMultiplier();
        //TODO: Make this buyable
        //TODO: PlayerEffect -> Buy? -> Add to Inventory

        public override ICommand PlayerEffect(IPlayer player)
        {
            //If link collides with, and he has enough money, Buy Item, else leave the item there
            //Link also needs enough space in inventory, or to already have one of this item in inventory
            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new NoOp();
        }

        public ICommand BuyItem()
        {
            //In the actual thing this will be triggered if Link has enough to buy it
            //Item added to inventory, collision cleared and sprite hidden
            //potentially monitored in ShopManager?
            //Applied immediately on purchase. Link now gets double? Triple? the amount of rupees per rupee
            return new NoOp();
        }
    }
}
