using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Fairy : Item
    {
        public Fairy(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateFairy();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new LinkFullHeal(player);
        }
    }
}
