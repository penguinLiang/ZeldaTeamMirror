using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    internal class Barrier : ICollideable, IDrawable
    {
        public Rectangle Bounds { get; }

        private readonly ISprite _sprite;

        public Barrier(Point location, BlockType block)
        {
            Bounds = CalculateBounds(location, block);
            _sprite = BlockTypeSprite.Sprite(block);
        }

        private static Rectangle CalculateBounds(Point location, BlockType block)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases (Most barriers are 32,32)
            switch (block)
            {
                case BlockType.DragonStatue:
                case BlockType.FishStatue:
                case BlockType.ImmovableBlock:
                case BlockType.InvisibleBlock:
                case BlockType.Fire:
                case BlockType.Water:
                    return new Rectangle(location, new Point(16, 16));
                case BlockType.BlackBarrier:
                    return new Rectangle(location, new Point(16, 24));
                default:
                    return new Rectangle(location, new Point(32, 32));
            }
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new MoveableHalt(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }

        public void Update()
        {
            _sprite?.Update();
        }

        public void Draw()
        {
            _sprite?.Draw(Bounds.Location.ToVector2());
        }
    }
}
