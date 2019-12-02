using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WhiteSwordItem : Item
    {
        private readonly int _price;
        private readonly DrawnText _priceDisplay;
        public WhiteSwordItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Text = _price.ToString(),
                Location = new Point(location.X, location.Y + 20)
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWhiteSword();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new UpgradeSword(player, Primary.WhiteSword);
                }

                Used = false;
                return new NoOp();
            }

            SoundEffectManager.Instance.PlayPickupNewItem();
            return new UpgradeSword(player, Primary.WhiteSword);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
