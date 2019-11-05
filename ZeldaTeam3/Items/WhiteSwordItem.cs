using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class WhiteSwordItem : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateWhiteSword();
        private readonly Vector2 _drawLocation;
        public Rectangle Bounds { get; private set; }

        public WhiteSwordItem(Point location)
        {
            var x = location.X;
            var y = location.Y;
            Bounds = new Rectangle(x + 8, y, 8, 16);
            _drawLocation = new Vector2(x, y);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            _sprite.Hide();
            Bounds = new Rectangle(0, 0, 0, 0);
            return new UpgradeSword(player, Primary.WhiteSword);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Update()
        {
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_drawLocation);
        }
    }
}
