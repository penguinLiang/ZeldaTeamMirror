
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Items;

namespace Zelda.Blocks
{
    internal class MovableBlock : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateSolidBlock();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;

        public MovableBlock(Point location)
        {
            var x = location.X;
            var y = location.Y;
            _bounds = new Rectangle(x + 8, y, 8, 8);
            _drawLocation = new Vector2(x + 8, y + 8);
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

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }

        public void MoveDown()
        {

        }
        public void MoveUp()
        {

        }
    }
}
