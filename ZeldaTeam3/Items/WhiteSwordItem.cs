using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WhiteSwordItem : Item
    {
        private int _price;
        public WhiteSwordItem(Point location, int price = 0) : base(location, price)
        {
         _price = price;   
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWhiteSword();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupNewItem();
            if(_price>0)
            {
                if(player.Inventory.TryRemoveRupee(_price))
                {
                    return new UpgradeSword(player, Primary.WhiteSword);
                }
                else
                {
                    Used = false;
                    return new NoOp();
                }
            }
            else
                return new UpgradeSword(player, Primary.WhiteSword);
        }
    }
}
