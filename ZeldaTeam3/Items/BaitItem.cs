using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BaitItem : Item
    {
        private readonly DrawnText _priceDisplay;
        private readonly int _price;
        private FrameDelay _delay = new FrameDelay(90, true);
        private bool _reset;
        private IPlayer _player;

        public BaitItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
            _priceDisplay = new DrawnText
            {
                Location = new Point(location.X, location.Y + 20),
                Text = _price.ToString()
            };
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBait();

        public override ICommand PlayerEffect(IPlayer player)
        {
            _delay.Update();
            _delay.Resume();
            Used = false;
            if (_price <= 0 || player.Inventory.ExtraItem1 != Secondary.None &&
                                player.Inventory.ExtraItem2 != Secondary.None) return new NoOp();

            _reset = false;
            _player = player;
            if (_delay.Delayed || !player.Inventory.TryRemoveRupee(_price)) return new NoOp();

            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkSecondaryAddAndAssign(player, Secondary.Bait);
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
            _priceDisplay.Draw();
            base.Draw();
        }
    }
}
