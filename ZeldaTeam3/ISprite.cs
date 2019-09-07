using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    // Based on: http://rbwhitaker.wikidot.com/monogame-texture-atlases-2
    public interface ISprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
