using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WhiteSwordItem : Item
    {
        public WhiteSwordItem(Point location) : base(location)
        {
            //price should not be 0 if survival mode shop
            System.Diagnostics.Debug.WriteLine("What is the price:", base.Price );
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWhiteSword();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupNewItem();
          //  if(Price>0)
            //check if you can buy it if price > 0
            //else
            return new UpgradeSword(player, Primary.WhiteSword);
        }
    }
}
