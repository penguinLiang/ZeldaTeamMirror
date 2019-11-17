using Zelda.Survival.GameWin;
using Zelda.Music;

namespace Zelda.Survival.GameState
{
    internal class GameWinWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] FixedDrawables { get; }

        public GameWinWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayWinMusic();

            var screen = new GameWinMenu(agent);
            Updatables = new IUpdatable[]
            {
                new GameWinControllerKeyboard(agent, screen),
                screen
            };
            FixedDrawables = new IDrawable[]
            {
                screen
            };
        }
    }
}