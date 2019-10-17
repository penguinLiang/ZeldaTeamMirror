using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, IDrawable, ICollideable
    {
    }
}
