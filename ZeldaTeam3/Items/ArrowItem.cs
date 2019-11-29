using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class ArrowItem : Item
    {
        private readonly Secondary _arrowLevel;

        protected override ISprite Sprite { get; }

        public ArrowItem(Point location, Secondary arrowLevel) : base(location)
        {
            _arrowLevel = arrowLevel;
            Sprite = arrowLevel == Secondary.Arrow ? ItemSpriteFactory.Instance.CreateArrow()
                : ItemSpriteFactory.Instance.CreateSilverArrow();
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupItem();
            switch (_arrowLevel)
            {
                case Secondary.Arrow:
                    return new AddSecondaryItem(player, Secondary.Arrow);
                case Secondary.SilverArrow:
                    return new AddSecondaryItem(player, Secondary.SilverArrow);
                default:
                    throw new System.ArgumentOutOfRangeException("Error: Items.Secondary _arrowLevel was not a type of arrow");
            }
        }
    }
}
