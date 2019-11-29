using Zelda.Survival.GameState;

namespace Zelda.Survival.Commands
{
    internal class ShowScoreboard : ICommand
    {
        private readonly GameStateAgent _agent;

        public ShowScoreboard(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.Scoreboard();
        }
    }
}
