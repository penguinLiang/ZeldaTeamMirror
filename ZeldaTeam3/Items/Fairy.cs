using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Fairy : ICollideable
    {
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;

        public Fairy(Point location)
        {
            var (x, y) = location;
            _bounds = new Rectangle(x + 8, y, 8, 16);
            _drawLocation = _bounds.Location.ToVector2();
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new LinkFullHeal(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            throw new System.NotImplementedException();
        }
        public ICommand ProjectileEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}
