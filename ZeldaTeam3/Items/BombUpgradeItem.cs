using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombUpgradeItem : Item
    {

        public int price { get; private set; }
        public BombUpgradeItem(Point location) : base(location)
        {
            price = 20;
            //TODO fix the prices later
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite
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
            //Applied Immediately on purchase. Link can now hold 2X bombs
            return new NoOp();
        }
    }
}
