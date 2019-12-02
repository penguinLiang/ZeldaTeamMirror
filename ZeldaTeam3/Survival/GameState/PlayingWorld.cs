using Zelda.Music;

namespace Zelda.Survival.GameState
{
    internal class PlayingWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] FixedDrawables { get; }
        public override IDrawable[] CameraDrawables { get; }

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

            FixedDrawables = new[]
            {
                StateAgent.HUD
            };

            CameraDrawables = new IDrawable[]
            {
                StateAgent.DungeonManager,
                StateAgent.Player
            };
        }
    }
}
