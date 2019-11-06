using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Map : Item
    {
        public Map(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddMap(player);
        }
    }
}
