using System;
using System.Collections.Generic;

namespace Zelda.Enemies
{
    internal class AgentStateUtility
    {
        private static readonly Random Rng = new Random();

        public static AgentState RandomFrom(List<AgentState> states)
        {
            return states[Rng.Next(states.Count)];
        }
    }
}
