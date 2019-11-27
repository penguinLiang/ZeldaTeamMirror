using Zelda.HighScore;
using Zelda.Music;

namespace Zelda.Survival.GameState
{
    internal class ScoreboardWorld : GameWorld
    {
        private IUpdatable[] _updatables;
        private IDrawable[] _cameraDrawables;
        public override IUpdatable[] Updatables => _updatables;
        public override IDrawable[] FixedDrawables => _cameraDrawables;

        private readonly Scoreboard _scoreboard = new Scoreboard();
        private readonly ScoreboardControllerKeyboard _controllerKeyboard;

        public ScoreboardWorld(GameStateAgent agent) : base(agent)
        {
            _controllerKeyboard = new ScoreboardControllerKeyboard(agent);
            _updatables = new IUpdatable[]
            {
                _controllerKeyboard, _scoreboard
            };
            _cameraDrawables = new IDrawable[]
            {
                _scoreboard
            };
        }
    }
}
