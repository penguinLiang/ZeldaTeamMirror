using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class ArrowItem : Item
    {
        public ArrowItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateArrow();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddArrow(player);
        }
    }
}
