using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class ATWBoomerangItem : Item
    {
        private DrawnText _priceDisplay;
        public int _price;
        public ATWBoomerangItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText();
            _priceDisplay.Location = new Point(location.X, location.Y + 20);
            _priceDisplay.Text = _price.ToString();
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateATWBoomerang();
       
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new LinkSecondaryAssign(player, Secondary.ATWBoomerang);
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new LinkSecondaryAssign(player, Secondary.ATWBoomerang);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
