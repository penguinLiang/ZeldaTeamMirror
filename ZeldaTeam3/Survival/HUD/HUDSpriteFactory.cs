using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Survival.HUD
{
    public class HUDSpriteFactory
    {
        public const int ScreenHeight = 48;
        public const int ScreenWidth = 256;

        private Texture2D _hudBackground;
        private Texture2D _hudSpriteSheet;

        public static HUDSpriteFactory Instance { get; } = new HUDSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _hudBackground = content.Load<Texture2D>("SurvivalHUD");
            _hudSpriteSheet = content.Load<Texture2D>("HUDElements");
        }

        public ISprite CreateBackground()
        {
            return new Sprite(_hudBackground, ScreenWidth, ScreenHeight, 1, new Point(0, 0));
        }
        public ISprite CreateFullHeart()
        {
            return new Sprite(_hudSpriteSheet, 8, 8, 1, new Point(32, 0));
        }
        public ISprite CreateHalfHeart()
        {
            return new Sprite(_hudSpriteSheet, 8, 8, 1, new Point(40, 0));
        }
        public ISprite CreateEmptyHeart()
        {
            return new Sprite(_hudSpriteSheet, 8, 8, 1, new Point(48, 0));
        }
    }
}
