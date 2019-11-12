using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class DroppedHeart : Item
    {
        public DroppedHeart(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateDroppedHeart();
        protected override Point Size { get; } = new Point(8, 8);

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupDroppedHeartKey();
            return new LinkHeal(player);
        }
    }
}
