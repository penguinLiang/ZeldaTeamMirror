namespace Zelda.Survival.GameState
{
    internal abstract class GameWorld
    {
        protected GameStateAgent StateAgent;

        protected GameWorld(GameStateAgent agent)
        {
            StateAgent = agent;
        }

        public virtual IUpdatable[] Updatables => new IUpdatable[] {};
        public virtual IDrawable[] FixedDrawables => new IDrawable[] {};
        public virtual IDrawable[] CameraDrawables => new IDrawable[] {};
        public virtual IDrawable[] UnscaledDrawables => new IDrawable[] {};
    }
}
