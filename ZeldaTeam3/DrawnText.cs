using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class DrawnText : IDrawable
    {
        private const int CharacterWidth = 7;

        public static SpriteBatch SpriteBatch { set; private get; }
        public static SpriteFont SpriteFont { set; private get; }

        public static int Width(string text) => CharacterWidth * text.Length;
        
        public string Text { set; private get; }
        public Point Location { get; set; }

        public void Update()
        {
            // NO-OP: No text animations
        }

        public void Draw()
        {
            SpriteBatch.DrawString(SpriteFont, Text, Location.ToVector2(), Color.White);
        }
    }
}
