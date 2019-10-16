using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface IEnemy : IHaltable, ISpawnable, IDrawable
    {
        //Rectangle Bounds { get; }
    }
}
