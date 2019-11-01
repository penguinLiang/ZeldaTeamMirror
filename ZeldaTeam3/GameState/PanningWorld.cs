namespace Zelda.GameState
{
    class PanningWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] ScaledDrawables { get; }

        public PanningWorld(GameStateAgent agent) : base(agent)
        {
            Updatables = new IUpdatable[]
            {
                StateAgent.HUD,
                StateAgent.DungeonManager
            };
            ScaledDrawables = new IDrawable[]
            {
                StateAgent.DungeonManager,
                StateAgent.HUD
            };
        }
    }
}
