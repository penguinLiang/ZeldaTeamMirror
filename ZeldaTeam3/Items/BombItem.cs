using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class BombItem : Item
    {
        public BombItem(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateBomb();

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            return new AddSecondaryItem(player, Secondary.Bomb);
        }
    }
}
