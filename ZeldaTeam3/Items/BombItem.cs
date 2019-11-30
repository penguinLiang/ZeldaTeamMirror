using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombItem : Item
    {
        private int _price;
        public BombItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBomb();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = false;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupItem();
                    return new AddSecondaryItem(player, Secondary.Bomb);
                }
                return new NoOp();
            }

            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.Bomb);
        }
    }
}
