
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    internal class DoorsAndStairs : ICollideable, IDrawable, IActivatable, IUpdatable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateRightOpenDoor();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;
        private BlockType _block;

        public DoorsAndStairs(Point location, BlockType block)
        {
            int x = location.X;
            int y = location.Y;
            _bounds = new Rectangle(x + 8, y, 8, 8);
            _drawLocation = new Vector2(x + 8, y + 8);
            _block = block;
            //Create the sprite from the block
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

        public void Activate()
        {
            return;
        }
    }
}
