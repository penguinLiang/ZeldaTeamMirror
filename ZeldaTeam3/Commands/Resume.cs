using Zelda.GameState;

namespace Zelda.Commands
{
    internal class Resume : ICommand
    {
        private readonly GameStateAgent _agent;

        public Resume(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.Resume();
        }
    }
}
