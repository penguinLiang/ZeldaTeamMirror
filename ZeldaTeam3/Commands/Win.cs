using Zelda.GameState;

namespace Zelda.Commands
{
    internal class Win : ICommand
    {
        private readonly GameStateAgent _agent;

        public Win(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.GameWin();
        }
    }
}
