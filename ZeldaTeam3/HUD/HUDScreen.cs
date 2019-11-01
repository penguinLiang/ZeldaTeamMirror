using Microsoft.Xna.Framework;

namespace Zelda.HUD
{
    // TODO: Fill in stub
    public class HUDScreen : IDrawable
    {
        private readonly ISprite _background = HUDSpriteFactory.Instance.CreateBackground();
        private Point _location;

        public HUDScreen(Point location)
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
