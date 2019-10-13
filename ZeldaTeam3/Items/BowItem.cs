
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class BowItem : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateDroppedHeart();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;

        public BowItem(Point location)
        {
            var (x, y) = location;
            _bounds = new Rectangle(x + 8, y, 8, 8);
            _drawLocation = new Vector2(x + 8, y + 8);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoO.Instance;
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
