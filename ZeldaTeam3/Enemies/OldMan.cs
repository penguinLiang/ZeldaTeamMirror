using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public class OldMan : EnemyAgent
    {
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);

        public override void Knockback()
        {
            // NO-OP
        }

        public override void Halt()
        {
            // NO-OP
        }

        public override void Stun()
        {
            // NO-OP
        }

        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateOldMan();
        public override bool Alive => true;
        private readonly ISprite _sprite;

        public OldMan(Point location)
        {
            Location = location;
            _sprite = EnemySpriteFactory.Instance.CreateOldMan();
            _sprite.Hide();
        }

        public override ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
        }

        public override void Spawn()
        {
            _sprite.Show();
        }

        public override void TakeDamage()
        {
            _sprite.PaletteShift();
        }

        public override void Draw()
        {
            _sprite.Draw(Location.ToVector2());
        }

        public override void Update()
        {
            _sprite.Update();
        }
    }
}
