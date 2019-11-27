using Zelda.HighScore;
using Zelda.Music;

namespace Zelda.Survival.GameState
{
    internal class ScoreboardWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] FixedDrawables { get; }

        public ScoreboardWorld(GameStateAgent agent) : base(agent)
        {
            Scoreboard scoreboard = new Scoreboard();
            Updatables = new IUpdatable[]
            {
                new ScoreboardControllerKeyboard(agent), scoreboard
            };
            FixedDrawables = new IDrawable[]
            {
                scoreboard
            };
        }
    }
}
