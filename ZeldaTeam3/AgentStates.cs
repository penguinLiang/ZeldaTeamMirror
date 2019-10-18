using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    /** 
     * Warning: Order matters to these enums as used by the RNG for the Agents. Do not change order of enums.
     */
    internal enum AgentStates
    {
        Ready,
        Moving,
        Halted,
        Attacking,
        Knocked
    }
}
