using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Triforce : Item
    {
        public Triforce(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateTriforcePiece();
        protected override Point Size { get; } = new Point(16, 16);
        protected override Point Offset { get; } = Point.Zero;
        protected override Point DrawOffset { get; } = Point.Zero;

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            player.TouchTriforce();
            
            return NoOp.Instance;
        }
    }
}
