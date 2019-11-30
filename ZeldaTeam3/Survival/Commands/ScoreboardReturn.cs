using Zelda.Survival.GameState;

namespace Zelda.Survival.Commands
{
    internal class ScoreboardReturn : ICommand
    {
        private readonly GameStateAgent _agent;

        public ScoreboardReturn(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.Play(); //Should go to main menu in finished game
        }
    }
}
