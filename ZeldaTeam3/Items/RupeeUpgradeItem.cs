using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class RupeeUpgradeItem : Item
    {
        private DrawnText _priceDisplay;
        private int _price;
        public RupeeUpgradeItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
           _priceDisplay = new DrawnText();
           _priceDisplay.Location = new Point(location.X, location.Y + 20);
           _priceDisplay.Text = _price.ToString();
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
                    player.Inventory.Rupee1Value = 3;
                    player.Inventory.Rupee5Value = 10;
                }
                else
                Used = false;
            }
            else
            {
                SoundEffectManager.Instance.PlayPickupItem();
                player.Inventory.Rupee1Value = 3;
                player.Inventory.Rupee5Value = 10;
            }
         return new NoOp();       
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
