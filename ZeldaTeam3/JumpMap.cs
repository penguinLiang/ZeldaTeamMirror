using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public class JumpMap
    {
        private readonly Texture2D _image;
        private readonly SpriteBatch _spriteBatch;

        public bool Visible;

        public JumpMap(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _image = content.Load<Texture2D>("JumpMap");
        }

        public void Draw()
        {
            if (Visible) _spriteBatch.Draw(_image, new Rectangle(0, 0, 512, 448), Color.White);
        }
    }
}
