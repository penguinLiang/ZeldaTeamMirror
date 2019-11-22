using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface IProjectile:  ICollideable, IHaltable, IDrawable
    {
        bool Halted { get; }

        void Reflect(List<Rectangle> orderedBounds);
    }
}
