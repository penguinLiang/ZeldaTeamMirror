using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface ISprite
    {
        bool AnimationFinished { get; }
        int Height { get; }
        int Width { get; }
        void Update();
        void Draw(Vector2 location);
        void Hide();
        void PauseAnimation();
        void PlayAnimation();
        void PaletteShift();
    }
}
