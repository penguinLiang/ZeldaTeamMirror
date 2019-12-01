using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class AlchemyCoinItem : Item
    {

        public int _price;
        public AlchemyCoinItem(Point location, int price = 0) : base(location, price)
        {
          _price = price;
        }
        
        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateAlchemyCoin();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.Coins);
                }
                else 
                {
                    Used = false;
                    return new NoOp();
                }
            }
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new AddSecondaryItem(player, Secondary.Coins);
        }
    }
}
