using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BowItem : Item
    {
        private readonly Secondary _bowLevel;
        private DrawnText priceDisplay;
        protected override ISprite Sprite { get; }
        private int _price;
        public BowItem(Point location, Secondary bowLevel, int price = 0) : base(location, price)
        {
            _price = price;
            _bowLevel = bowLevel;
            Sprite = bowLevel == Secondary.Bow ? ItemSpriteFactory.Instance.CreateBow()
                : ItemSpriteFactory.Instance.CreateFireBow();
            priceDisplay = new DrawnText();
            priceDisplay.Location = new Point(location.X, location.Y + 20);
            priceDisplay.Text = _price.ToString();
        }
        
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;

            if(_price>0)
            { 
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.Bow);
                }
                    Used = false;
                    return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new AddSecondaryItem(player, Secondary.Bow);
        }

        public override void Draw()
        {
            if(_price>0)
            {
                priceDisplay.Draw();    
            }
            base.Draw();
        }
    }
}
