using System;
using Zelda;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public class JumpMap
    {
        Texture image;

        public JumpMap(ContentManager content)
        {
            image = content.Load<Texture2D>("JumpMap");
        }
    }
}
