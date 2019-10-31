using Zelda.JumpMap;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class JumpMapWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] UnscaledDrawables { get; }

        public JumpMapWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PauseMusic();
            Updatables = new IUpdatable[]
            {
                new JumpMapControllerMouse(StateAgent),
                new JumpMapControllerKeyboard(StateAgent)
            };
            UnscaledDrawables = new IDrawable[]
            {
                new JumpMapScreen()
            };
        }
    }
}
