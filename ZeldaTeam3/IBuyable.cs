using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
   public interface IBuyable
    {
        int price { get; set; }
        ICommand Buy(IBuyable item);
        bool isPurchased { get; set; }

    }
}
