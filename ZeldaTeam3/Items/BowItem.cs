using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class BowItem : Item
    {
        public BowItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBow();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddSecondaryItem(player, Secondary.Bow);
        }
    }
}
