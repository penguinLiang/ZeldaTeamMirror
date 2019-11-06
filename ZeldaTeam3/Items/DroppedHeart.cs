using Microsoft.Xna.Framework;
using Zelda.Commands;

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
            return new LinkHeal(player);
        }
    }
}
