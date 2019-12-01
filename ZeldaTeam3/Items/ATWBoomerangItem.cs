using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class ATWBoomerangItem : Item
    {
        private DrawnText priceDisplay;
        public int _price;
        public ATWBoomerangItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
            priceDisplay = new DrawnText();
            priceDisplay.Location = new Point(location.X, location.Y + 20);
            priceDisplay.Text = _price.ToString();
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
            priceDisplay.Draw();
            base.Draw();
        }
    }
}
