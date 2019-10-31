using System;
using Zelda.GameState;

namespace Zelda.Commands
{
    internal class Reset : ICommand
    {
        private readonly GameStateAgent _agent;

        public Reset(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            Console.WriteLine("WHAT UP BITCHES");
            _agent.Reset();
        }

        public override string ToString() => "Reset the game";
    }
}
