
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class ArrowItem : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateArrow();
        private readonly Vector2 _drawLocation;
        public Rectangle Bounds { get; private set; }

        public ArrowItem(Point location)
        {
            Bounds = new Rectangle(location.X + 8, location.Y, 8, 16);
            _drawLocation = new Vector2(location.X + 8, location.Y + 8);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            _sprite.Hide();
            Bounds = new Rectangle(0, 0, 0, 0);
            return new AddArrow(player);
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
