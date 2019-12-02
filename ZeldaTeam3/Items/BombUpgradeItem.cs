using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombUpgradeItem : Item
    {
        private DrawnText _priceDisplay;
        public int _price;
        public BombUpgradeItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText();
            _priceDisplay.Text = _price.ToString();
            _priceDisplay.Location = new Point(location.X, location.Y + 20);
        }
        
        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBombWalletUpgrade();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    player.Inventory.UpgradeBombWallet(player.Inventory.MaxBombCount * 2);
                    return new NoOp();
                }
                    Used = false;
                    return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupItem();
            player.Inventory.UpgradeBombWallet(player.Inventory.MaxBombCount * 2);
            return new NoOp();
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }

    }
}
