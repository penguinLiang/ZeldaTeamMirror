using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WhiteSwordItem : Item
    {
        private int _price;
        private DrawnText priceDisplay;
        public WhiteSwordItem(Point location, int price = 0) : base(location, price)
        {
         _price = price;   
         priceDisplay = new DrawnText();
         priceDisplay.Text = _price.ToString();
         priceDisplay.Location = new Point(location.X, location.Y+20);
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWhiteSword();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new UpgradeSword(player, Primary.WhiteSword);
                }
                else
                {
                    Used = false;
                    return new NoOp();
                }
            }
            else
               {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new UpgradeSword(player, Primary.WhiteSword);
                }
        }

        public override void Draw()
        {
            priceDisplay.Draw();
            base.Draw();
        }
    }
}
