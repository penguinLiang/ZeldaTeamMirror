using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombLauncherItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        public BombLauncherItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Text = _price.ToString(),
                Location = new Point(location.X, location.Y + 20)
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBombLauncher();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.BombLauncher);
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.BombLauncher);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
