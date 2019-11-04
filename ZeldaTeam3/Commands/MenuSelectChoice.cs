using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda.Commands
{
   internal class MenuSelectChoice :ICommand
    {
            private readonly IMenu _menu;

            public MenuSelectChoice(IMenu menu)
            {
                _menu = menu;
            }

            public void Execute()
            {
                _menu.Choose();
            }
        

    }
}
