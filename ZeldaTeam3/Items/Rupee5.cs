﻿using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class Rupee5 : Item
    {
        public Rupee5(Point location) : base(location)
        {
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.Create5Rupee();
        protected override Point Size { get; } = new Point(16, 16);
        protected override Point Offset { get; } = Point.Zero;
        protected override Point DrawOffset { get; } = Point.Zero;

        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            SoundEffectManager.Instance.PlayPickupRupee();
            return new Add5Rupee(player);
        }
    }
}
