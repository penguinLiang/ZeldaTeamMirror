
using Microsoft.Xna.Framework;
using Zelda.Commands;


namespace Zelda.Blocks
{
    internal class MovableBlock : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateSolidBlock();
        public Rectangle Bounds { get; }

        public MovableBlock(Point location)
        {
            Bounds = new Rectangle(location, new Point(16, 16));
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
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
            _sprite.Draw(Bounds.Location.ToVector2());
        }

        public void MoveLeft()
        {
            return; 
        }

        public void MoveRight()
        {
            return;
        }

        public void MoveDown()
        {
            return;
        }
        public void MoveUp()
        {
            return;
        }
    }
}
