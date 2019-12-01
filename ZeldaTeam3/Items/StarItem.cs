using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class StarItem : Item
    {
        private DrawnText _priceDisplay;
        private readonly FrameDelay _delay = new FrameDelay(90);
        private int _price;
        public StarItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
           _priceDisplay = new DrawnText();
           _priceDisplay.Text = _price.ToString();
           _priceDisplay.Location = new Point(location.X, location.Y + 20);
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite

        public override ICommand PlayerEffect(IPlayer player)
        {
            _delay.Update();
            Used = true;
            if(_price>0)
            {
                if(!_delay.Delayed && ((player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None) && player.Inventory.TryRemoveRupee(_price)))
                {
                        player.Inventory.AssignSecondaryItem(Secondary.Star);
                        SoundEffectManager.Instance.PlayPickupItem();
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAssign(player, Secondary.Star);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }

    }
}
