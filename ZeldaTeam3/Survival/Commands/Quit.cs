using Zelda.Survival.GameState;

namespace Zelda.Survival.Commands
{
    internal class Quit : ICommand
    {
        private readonly GameStateAgent _agent;

        public Quit(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.Quit();
        }

        public override string ToString() => "Quit the game";
    }
}
