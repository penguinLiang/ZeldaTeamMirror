using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    internal class LeftRightDoor : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; }

        private readonly Vector2 _drawLocation;

        public LeftRightDoor(Point location, BlockType block)
        {
            Bounds = new Rectangle(location.X, location.Y, 32, 48);
            _sprite = BlockTypeSprite.Sprite(block);
            _drawLocation = new Vector2(location.X, location.Y + 8);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new DoorLinkKnockback(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw()
        {
            _sprite?.Draw(_drawLocation);
        }

        public void Activate()
        {
            // NO-OP
        }
    }
}
