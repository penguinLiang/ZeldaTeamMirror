
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Items;

namespace Zelda.Blocks
{
    internal class Barrier : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateDroppedHeart();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;
        private BlockType _block;

        public Barrier(Point location, BlockType block)
        {
            var x = location.X;
            var y = location.Y;
            _bounds = new Rectangle(x + 8, y, 8, 8);
            _drawLocation = new Vector2(x + 8, y + 8);
            _block = block;
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
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
