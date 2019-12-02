using Microsoft.Xna.Framework;
using Zelda.Survival.HUD;
using Zelda.Survival.Pause;

namespace Zelda.Survival.GameState
{
    internal class PausedWorld : GameWorld
    {
        private const int YOffset = HUDSpriteFactory.ScreenHeight + PauseSpriteFactory.ScreenHeight;

        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] FixedDrawables { get; }
        public override IDrawable[] CameraDrawables { get; }

        public PausedWorld(GameStateAgent agent) : base(agent)
        {
            var pause = new PauseMenu(agent, new Point(0, -YOffset));
            Updatables = new IUpdatable[]
            {
                new ControllerPauseKeyboard(StateAgent, pause), 
                StateAgent.HUD,
                pause
            };
            FixedDrawables = new[]
            {
                StateAgent.HUD,
                pause
            };
            CameraDrawables = new IDrawable[]
            {
                StateAgent.DungeonManager
            };
        }
    }
}
