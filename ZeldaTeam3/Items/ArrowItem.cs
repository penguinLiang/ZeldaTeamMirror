using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

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
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddArrow(player);
        }
    }
}
