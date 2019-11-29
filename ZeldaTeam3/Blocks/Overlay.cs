using Microsoft.Xna.Framework;
using Zelda.ShaderEffects;

namespace Zelda.Blocks
{
    internal class Overlay : IDrawable
    {
        private readonly ISprite _sprite;
        private Point _location;

        public Overlay(Point location, BlockType block)
        {
            _location = location;
            _sprite = new AlphaPassMask(BlockTypeSprite.Sprite(block), false);
        }

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw()
        {
            _sprite?.Draw(_location.ToVector2());
        }
    }
}
