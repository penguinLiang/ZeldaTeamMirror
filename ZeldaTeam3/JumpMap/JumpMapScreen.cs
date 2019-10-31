using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.JumpMap
{
    public class JumpMapScreen : IDrawable
    {
        public static ISprite Image { get; private set; }

        public static void LoadTexture(ContentManager content)
        {
            Image = new Sprite(content.Load<Texture2D>("JumpMap"), 512, 448, 1, Point.Zero);
        }

        public void Update()
        {
            // NO-OP: Jump map is static
        }

        public void Draw()
        {
            Image.Draw(Vector2.Zero);
        }
    }
}
