using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WalletUpgradeItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        public WalletUpgradeItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Location = new Point(location.X, location.Y + 20),
                Text = _price.ToString()
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWalletUpgrade();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    player.Inventory.UpgradeRupeeWallet(player.Inventory.MaxRupeeCount * 2);
                    return NoOp.Instance;
                }

                Used = false;
                return NoOp.Instance;
            }
            SoundEffectManager.Instance.PlayPickupItem();
            player.Inventory.UpgradeRupeeWallet(player.Inventory.MaxRupeeCount * 2);
            return NoOp.Instance;
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
