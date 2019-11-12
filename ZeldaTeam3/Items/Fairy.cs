using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

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
            SoundEffectManager.Instance.PlayPickupItem();
            return new LinkFullHeal(player);
        }
    }
}
