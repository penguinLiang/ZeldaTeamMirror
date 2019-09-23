using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public interface ISprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 location);
        void Show();
        void Hide();
        void PauseAnimation();
        void PlayAnimation();
        void PaletteShift();
    }
}
