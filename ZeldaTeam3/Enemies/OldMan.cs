using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public class OldMan : EnemyAgent
    {
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateOldMan();
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        public override bool Alive => true;
        public override void Halt()
        {
            // NO-OP: Old man doesn't move
        }

        public OldMan(Point location)
        {
            Location = location + new Point(8, 0);
            
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
