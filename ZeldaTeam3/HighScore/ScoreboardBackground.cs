using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.HighScore
{
    public class ScoreboardBackground : IDrawable
    {
        private static readonly Vector2 LocationAfterHUDOffset = new Vector2(0, -48);

        public static ISprite Image { get; private set; }

        public static void LoadTexture(ContentManager content)
        {
            Image = new Sprite(content.Load<Texture2D>("Scoreboard"), 256, 224, 1, Point.Zero);
        }

        public void Update()
        {
            // NO-OP: Scoreboard image is static
        }

        public void Draw()
        {
            Image.Draw(LocationAfterHUDOffset);
        }
    }
}
