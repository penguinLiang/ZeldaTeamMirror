using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class ATWBoomerangItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        public ATWBoomerangItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Location = new Point(location.X, location.Y + 20),
                Text = _price.ToString()
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateATWBoomerang();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if (_price > 0)
            {
                if (player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new LinkSecondaryAddAndAssign(player, Secondary.ATWBoomerang);
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new LinkSecondaryAddAndAssign(player, Secondary.ATWBoomerang);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
