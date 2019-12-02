using Microsoft.Xna.Framework;
using Zelda.HUD;
using Zelda.Pause;

namespace Zelda.GameState
{
    internal class PausedWorld : GameWorld
    {
        private const int YOffset = HUDSpriteFactory.ScreenHeight + PauseSpriteFactory.ScreenHeight;

        public override IUpdatable[] Updatables { get; }
        public override IDrawable[] ScaledDrawables { get; }

        public PausedWorld(GameStateAgent agent) : base(agent)
        {
            var pause = new PauseMenu(agent, new Point(0, -YOffset));
            Updatables = new IUpdatable[]
            {
                new ControllerPauseKeyboard(StateAgent, pause), 
                StateAgent.HUD,
                pause
            };
            ScaledDrawables = new[]
            {
                StateAgent.DungeonManager,
                StateAgent.HUD,
                pause
            };
        }
    }
}
