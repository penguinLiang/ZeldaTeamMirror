using Microsoft.Xna.Framework;
using Zelda.HUD;

namespace Zelda.GameOver
{
    internal class GameOverScreen : IDrawable
    {
        private const string GameOverMessage = "GAME OVER";
        private static readonly Point GameOverMessageLocation =
            new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(GameOverMessage)) / 2, 0);

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText() { Location = GameOverMessageLocation, Text = GameOverMessage }
        };

        public GameOverScreen()
        {
        }

        public void Update()
        {
        }

        public void Draw()
        {
            foreach (var textDrawable in _textDrawables)
            {
                textDrawable.Draw();
            }
        }
    }
}
