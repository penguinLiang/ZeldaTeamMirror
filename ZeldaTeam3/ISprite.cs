using Microsoft.Xna.Framework;

namespace Zelda
{
    public interface ISprite
    {
        bool AnimationFinished { get; }
        void Update();
        void Draw(Vector2 location);
        void Show();
        void Hide();
        void PauseAnimation();
        void PlayAnimation();
        void PaletteShift();
    }
}
