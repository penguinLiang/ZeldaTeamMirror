using Zelda.Music;

namespace Zelda.GameState
{
    internal class PlayingWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] ScaledDrawables { get; }

        public PlayingWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayLabryinthMusic();
            Updatables = new IUpdatable[]
            {
                new ControllerKeyboard(StateAgent),
                StateAgent.Player,
                StateAgent.HUD,
                StateAgent.DungeonManager 
            };

            ScaledDrawables = new IDrawable[]
            {
                StateAgent.DungeonManager, 
                StateAgent.Player,
                StateAgent.HUD
            };
        }
    }
}
