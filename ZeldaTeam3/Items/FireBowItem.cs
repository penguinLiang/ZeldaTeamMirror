using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class FireBowItem : Item
    {
        private DrawnText priceDisplay;
        private int _price;
        public FireBowItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            priceDisplay = new DrawnText();
            priceDisplay.Text = _price.ToString();
            priceDisplay.Location = new Point(location.X, location.Y + 20);
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.FireBow);
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.FireBow);  
        }

        public override void Draw()
        {
            priceDisplay.Draw();
            base.Draw();
        }
    }
}
