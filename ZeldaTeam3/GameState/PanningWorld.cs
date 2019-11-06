namespace Zelda.GameState
{
    internal class PanningWorld : GameWorld
    {
        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] ScaledDrawables { get; }

        public PanningWorld(GameStateAgent agent) : base(agent)
        {
            Updatables = new IUpdatable[]
            {
                new QuitResetControllerKeyboard(agent), 
                StateAgent.HUD,
            };
            ScaledDrawables = new IDrawable[]
            {
                StateAgent.HUD,
            };
        }
    }
}
