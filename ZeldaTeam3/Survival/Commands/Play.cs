using Zelda.Survival.GameState;

namespace Zelda.Survival.Commands
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
