using Zelda.GameState;

namespace Zelda.Commands
{
    internal class Play : ICommand
    {
        private readonly GameStateAgent _agent;

        public Play(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.Play();
        }
    }
}
