using Zelda.GameState;

namespace Zelda.Commands
{
    internal class ShowJumpMap : ICommand
    {
        private readonly GameStateAgent _agent;

        public ShowJumpMap(GameStateAgent agent)
        {
            _agent = agent;
        }

        public void Execute()
        {
            _agent.JumpMap();
        }
    }
}
