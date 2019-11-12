using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class WhiteSwordItem : Item
    {
        public WhiteSwordItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWhiteSword();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new UpgradeSword(player, Primary.WhiteSword);
        }
    }
}
