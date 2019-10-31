using Microsoft.Xna.Framework;

namespace Zelda.Pause
{
    // TODO: Replace stub with implementation
    class PauseScreen : IDrawable
    {
        private readonly ISprite _background = PauseSpriteFactory.Instance.CreateBackground();
        private Point _location;

        public PauseScreen(Point location)
        {
            _location = location;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            _background.Draw(_location.ToVector2());
        }
    }
}
