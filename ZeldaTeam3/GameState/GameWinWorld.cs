using Zelda.GameWin;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class GameWinWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] ScaledDrawables { get; }

        public GameWinWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayWinMusic();

            var screen = new GameWinMenu(agent);
            Updatables = new IUpdatable[]
            {
                new GameWinControllerKeyboard(agent, screen), 
               screen
            };
            ScaledDrawables = new IDrawable[]
            {
                screen
            };
        }
    }
}
