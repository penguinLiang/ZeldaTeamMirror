using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class MagicSwordItem : Item
    {
        private int _price;
        public MagicSwordItem(Point location, int price = 0) : base(location, price)
        {
            _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMagicSword();
        protected override Point Offset { get; } = new Point(8, 0);
        protected override Point DrawOffset { get; } = new Point(8, 0);

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new UpgradeSword(player, Primary.MagicalSword);
                }
                    Used = false;
                    return new NoOp();
            } 
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new UpgradeSword(player, Primary.MagicalSword);
        }
    }
}
