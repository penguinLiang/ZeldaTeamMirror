using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Key : Item
    {
        public Key(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateKey();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddKey(player);
        }
    }
}
