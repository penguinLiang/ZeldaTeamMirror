using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public class OldMan : EnemyAgent
    {
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateOldMan();
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        public override bool Alive => true;
        public override List<IProjectile> Projectiles { get; set; }
        public override void Halt()
        {
            // NO-OP: Old man doesn't move
        }

        public OldMan(Point location)
        {
            Location = location + new Point(8, 0);
            Projectiles = new List<IProjectile>();
            //when he attacks, add the projectiles to the array
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
        }

        public override void TakeDamage()
        {
            Sprite.PaletteShift();
        }

        public override void Update()
        {
            Sprite.Update();
        }

        public override void Draw()
        {
            Sprite.Draw(Location.ToVector2());
        }
    }
}
