using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class RupeeUpgradeItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        public RupeeUpgradeItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Location = new Point(location.X, location.Y + 20),
                Text = _price.ToString()
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateRupeeMultiplier();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    player.Inventory.UpgradeRupeeMultiplier(3);
                    return NoOp.Instance;
                }
                Used = false;
            }
            else
            {
                SoundEffectManager.Instance.PlayPickupItem();
                player.Inventory.UpgradeRupeeMultiplier(3);
            }
            return NoOp.Instance;
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
