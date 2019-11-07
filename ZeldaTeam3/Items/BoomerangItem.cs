using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class BoomerangItem : Item
    {
        public BoomerangItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateWoodBoomerang();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddSecondaryItem(player, Secondary.Boomerang);
        }
    }
}
