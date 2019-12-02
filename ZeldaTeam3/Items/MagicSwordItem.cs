using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class MagicSwordItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        public MagicSwordItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Location = new Point(location.X, location.Y + 20),
                Text = _price.ToString()
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMagicSword();
        protected override Point Offset { get; } = new Point(8, 0);
        protected override Point DrawOffset { get; } = new Point(8, 0);

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new UpgradeSword(player, Primary.MagicalSword);
                }
                Used = false;
                return NoOp.Instance;
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new UpgradeSword(player, Primary.MagicalSword);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
