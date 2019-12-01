using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class ArrowItem : Item
    {
        private DrawnText _priceDisplay;
        private readonly Secondary _arrowLevel;
        private int _price;

        protected override ISprite Sprite { get; }

        public ArrowItem(Point location, Secondary arrowLevel, int price = 0) : base(location, price)
        {
            _arrowLevel = arrowLevel;
            _price = price;
            Sprite = arrowLevel == Secondary.Arrow ? ItemSpriteFactory.Instance.CreateArrow()
                : ItemSpriteFactory.Instance.CreateSilverArrow();
            _priceDisplay = new DrawnText();
            _priceDisplay.Text = _price.ToString();
            _priceDisplay.Location = new Point(location.X, location.Y + 20);
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            switch (_arrowLevel)
            {
                case Secondary.Arrow:
                    if(_price>0 && player.Inventory.TryRemoveRupee(_price))
                    {
                        SoundEffectManager.Instance.PlayPickupItem();
                        return new AddSecondaryItem(player, Secondary.Arrow); 
                    }
                    else if(_price>0) 
                    {
                        Used = false;
                    }
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new AddSecondaryItem(player, Secondary.Arrow);
                case Secondary.SilverArrow:
                       if(_price>0 && player.Inventory.TryRemoveRupee(_price))  
                        {
                            SoundEffectManager.Instance.PlayPickupItem();
                            return new AddSecondaryItem(player, Secondary.SilverArrow); 
                        }
                    else if(_price>0) 
                    {
                        Used = false;
                    }
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new AddSecondaryItem(player, Secondary.SilverArrow);
                default:
                    throw new System.ArgumentOutOfRangeException("Error: Items.Secondary _arrowLevel was not a type of arrow");
            }
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
