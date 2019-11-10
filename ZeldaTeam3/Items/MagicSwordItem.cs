using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class MagicSwordItem : Item
    {
        public MagicSwordItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMagicSword();
        protected override Point Offset { get; } = new Point(8, 0);
        protected override Point DrawOffset { get; } = new Point(8, 0);

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupNewItem();
            return new UpgradeSword(player, Primary.MagicalSword);
        }
    }
}
