using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.ModeMenu
{
    public class MainMenuBackground : IDrawable
    {
        private static readonly Vector2 LocationAfterHUDOffset = new Vector2(0, -HUD.HUDSpriteFactory.ScreenHeight);

        public static ISprite Image { get; private set; }

        public static void LoadTexture(ContentManager content)
        {
            Image = new Sprite(content.Load<Texture2D>("MainMenu"), 256, 224, 1, Point.Zero);
        }

        public void Update()
        {
            // NO-OP: Menu image is static
        }

        public void Draw()
        {
            Image.Draw(Vector2.Zero);
        }
    }
}
