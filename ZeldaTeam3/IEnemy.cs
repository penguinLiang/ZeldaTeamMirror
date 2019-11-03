using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, ICollideable
    {
        void Draw();
        void Update(Point playerLocation);
    }
}
