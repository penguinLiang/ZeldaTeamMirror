using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombItem : Item
    {
        private int _price;
        private readonly FrameDelay _delay = new FrameDelay(90);
        public BombItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
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
           // _delay.Update();
            base.Update();
        }
    }
}
