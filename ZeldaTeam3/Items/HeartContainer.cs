using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class HeartContainer : Item
    {
        public HeartContainer(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateHeartContainer();
        protected override Point Size { get; } = new Point(16, 16);
        protected override Point Offset { get; } = Point.Zero;
        protected override Point DrawOffset { get; } = Point.Zero;

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new LinkAddHeart(player);
        }
    }
}
