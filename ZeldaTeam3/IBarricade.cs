using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
   public interface IBarricade: ICollideable, IDrawable
    {
         bool unlocked { get; set; }
         void Unlock();
         void Reset();
    }
}
