using Microsoft.Xna.Framework;
using Zelda.HUD;
using Zelda.Music;

namespace Zelda.GameState
{
    internal class GameWinWorld : GameWorld
    {
        private const string WinMessage = "YOU WIN!";
        private const string PlayAgainMessage = "...Play Again?";
        private static readonly int ScreenHeight = 224;
        private static readonly Point WinMessageLocation = new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(WinMessage)) / 2, 0);
        private static readonly Point PlayAgainMessageLocation = new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(PlayAgainMessage)) / 2, HUDSpriteFactory.ScreenHeight+64);

        
        public override IDrawable[] ScaledDrawables { get; }
        public GameWinWorld(GameStateAgent agent) : base(agent)
        {
            MusicManager.Instance.PlayWinMusic();
            ScaledDrawables = new IDrawable[]
            {
                new DrawnText { Location =  WinMessageLocation, Text = WinMessage },

                new DrawnText {Location = PlayAgainMessageLocation, Text = PlayAgainMessage}
            };
        }
    }
}
