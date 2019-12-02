using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class LaserBeamItem : Item
    {
        private DrawnText _priceDisplay;
        private readonly FrameDelay _delay = new FrameDelay(90);
        private int _price; 
        public LaserBeamItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText();
            _priceDisplay.Location = new Point(location.X, location.Y + 20);
            _priceDisplay.Text = _price.ToString();
        }
        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateLaserBeam();

        public override ICommand PlayerEffect(IPlayer player)
        {
            _delay.Update();
            Used = false;
            if(_price>0)
            {
                if(!_delay.Delayed && ((player.Inventory.ExtraItem1 == Secondary.None || player.Inventory.ExtraItem2 == Secondary.None) && player.Inventory.TryRemoveRupee(_price)))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new LinkSecondaryAssign(player, Secondary.LaserBeam);
                }
                    return new NoOp();
            }
            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAssign(player, Secondary.LaserBeam);
        }

        public override void Draw()
        {
            _priceDisplay.Draw();
            base.Draw();
        }

    }
}
