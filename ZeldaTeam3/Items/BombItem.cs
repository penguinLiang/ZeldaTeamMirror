using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        private FrameDelay _delay = new FrameDelay(90, true);
        private bool _reset;
        private IPlayer _player;

        public BombItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Text = _price.ToString(),
                Location = new Point(location.X, location.Y + 20)
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBomb();

        public override ICommand PlayerEffect(IPlayer player)
        {
            _delay.Update();
            _delay.Resume();
            Used = false;
            if (_price > 0)
            {
                _reset = false;
                _player = player;
                if (_delay.Delayed || player.Inventory.BombCount == player.Inventory.MaxBombCount ||
                    !player.Inventory.TryRemoveRupee(_price)) return new NoOp();

                SoundEffectManager.Instance.PlayPickupItem();
                return new AddSecondaryItem(player, Secondary.Bomb);
            }

            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.Bomb);
        }

        public override void Update()
        {
            base.Update();
            if (_player == null || _reset || _player.BodyCollision.CollidesWith(Bounds)) return;
            _delay = new FrameDelay(90, true);
            _reset = true;
        }

        public override void Draw()
        {
            if (_price > 0)
            {
                _priceDisplay.Draw();
            }
            base.Draw();
        }
    }
}
