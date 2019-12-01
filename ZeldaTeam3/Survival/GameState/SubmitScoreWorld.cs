using Zelda.HighScore;

namespace Zelda.Survival.GameState
{
    internal class SubmitScoreWorld : GameWorld
    {
        private readonly GameStateAgent _agent;
        private IUpdatable[] _updatables;
        private IDrawable[] _fixedDrawables;
        public override IUpdatable[] Updatables => _updatables;
        public override IDrawable[] FixedDrawables => _fixedDrawables;

        private void HandleSubmit(string initials)
        {
            var scoreboard = new Scoreboard(_agent.Score, initials);
            _updatables = new IUpdatable[]
            {
                new ScoreboardControllerKeyboard(_agent), 
                scoreboard
            };
            _fixedDrawables = new IDrawable[]
            {
                scoreboard
            };
        }

        public SubmitScoreWorld(GameStateAgent agent) : base(agent)
        {
            _agent = agent;
            var entryScreen = new InitialEntryScreen(agent.Score) { OnSubmit = HandleSubmit };
            _updatables = new IUpdatable[]
            {
                entryScreen
            };
            _fixedDrawables = new IDrawable[]
            {
                entryScreen
            };
        }
    }
}
