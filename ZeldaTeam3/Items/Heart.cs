using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Items
{
    internal class Heart : ICollideable
    {
        Rectangle _tileSpace;

        public Heart(Vector2 location)
        {
            _tileSpace = new Rectangle((int)location.X,(int)location.Y, 16, 16);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _tileSpace.Intersects(rect);
        }
        public ICommand PlayerEffect(IPlayer player)
        {
            return new LinkHeal(player);
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
