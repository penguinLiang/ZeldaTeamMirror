
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Triforce : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateTriforcePiece();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;

        public Triforce(Point location)
        {
            var x = location.X;
            var y = location.Y;
            _bounds = new Rectangle(x, y, 16, 16);
            _drawLocation = new Vector2(x + 8, y + 8);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            _sprite.Hide();
            _bounds = new Rectangle(0, 0, 0, 0);
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable projectile)
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
