using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WalletUpgradeItem : Item
    {
        private int _price;
        public WalletUpgradeItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }
        
        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWalletUpgrade();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    player.Inventory.UpgradeRupeeWallet(player.Inventory.MaxRupeeCount * 2);
                    return new NoOp();
                }
                else 
                {
                    Used = false;
                    return new NoOp();
                }
            }
            SoundEffectManager.Instance.PlayPickupItem();
            player.Inventory.UpgradeRupeeWallet(player.Inventory.MaxRupeeCount * 2);
            return new NoOp();
        }
    }
}
