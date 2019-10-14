using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Fairy : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = ItemSpriteFactory.Instance.CreateFairy();
        private readonly Vector2 _drawLocation;
        private Rectangle _bounds;

        public Fairy(Point location)
        {
            _bounds = new Rectangle(location.X + 8, location.Y, 8, 16);
            _drawLocation = _bounds.Location.ToVector2();
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
             _sprite.Hide();
            _bounds = new Rectangle(0, 0, 0, 0);
            return new LinkFullHeal(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable haltable)
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
