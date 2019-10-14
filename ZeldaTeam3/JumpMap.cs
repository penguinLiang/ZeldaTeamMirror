using System;
using Zelda;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public class JumpMap
    {
        private Texture2D image;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;


        public JumpMap(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            image = content.Load<Texture2D>("JumpMap");            
        }

        public void Draw()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(image, new Rectangle(0, 0, 512, 448), Color.Black);
            _spriteBatch.End();
        }
    }
}
