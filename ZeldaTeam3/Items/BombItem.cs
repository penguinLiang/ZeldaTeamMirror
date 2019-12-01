using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombItem : Item
    {
        private DrawnText priceDisplay;
        private int _price;
        private readonly FrameDelay _delay = new FrameDelay(90);
        public BombItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            priceDisplay = new DrawnText();
            priceDisplay.Text = _price.ToString();
            priceDisplay.Location = new Point(location.X, location.Y + 20);
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBomb();

        public override ICommand PlayerEffect(IPlayer player)
        {
            _delay.Update();
            Used = false;
            if(_price>0 && player.Inventory.BombCount<player.Inventory.MaxBombCount)
            {
                if(!_delay.Delayed && player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new AddSecondaryItem(player, Secondary.Bomb);
                }
                return new NoOp();
            }
            else if(_price == 0)
            {
                Used = true;
                SoundEffectManager.Instance.PlayPickupItem();
                return new AddSecondaryItem(player, Secondary.Bomb);
            }
            else 
                return new NoOp();
        }

        public override void Update()
        {
            base.Update();
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
