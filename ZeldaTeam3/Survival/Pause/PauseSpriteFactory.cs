using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Survival.Pause
{
    public class PauseSpriteFactory
    {
        public const int ScreenWidth = 256;
        public const int ScreenHeight = 176;

        private Texture2D _pauseBackground;
        private Texture2D _pauseSpriteSheet;

        public static PauseSpriteFactory Instance { get; } = new PauseSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _pauseBackground = content.Load<Texture2D>("SurvivalPauseScreen");
            _pauseSpriteSheet = content.Load<Texture2D>("HUDElements");
        }

        public ISprite CreateBackground()
        {
            return new Sprite(_pauseBackground, ScreenWidth, ScreenHeight, 1, new Point(0, 0));
        }

        public ISprite CreateCursorFrame()
        {
            return new Sprite(_pauseSpriteSheet, 16, 16, 2, new Point(0, 0), 10);
        }
    }
}
