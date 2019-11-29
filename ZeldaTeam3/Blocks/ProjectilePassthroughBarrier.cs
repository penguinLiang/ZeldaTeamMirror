using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    internal class ProjectilPassthroughBarrier : ICollideable, IDrawable
    {
        public Rectangle Bounds { get; }

        private readonly ISprite _sprite;
        private Point _location;

        public ProjectilPassthroughBarrier(Point location, BlockType block)
        {
            Bounds = CalculateBounds(location, block);
            _location = location;
            _sprite = BlockTypeSprite.Sprite(block);
        }

        private static Rectangle CalculateBounds(Point location, BlockType block)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (block)
            {
                case BlockType.Fire:
                case BlockType.DragonStatue:
                case BlockType.FishStatue:
                case BlockType.ImmovableBlock:
                case BlockType.Water:
                case BlockType.ProjectileBlackBarrier:
                    return new Rectangle(location, new Point(16, 16));
                default:
                    throw new ArgumentOutOfRangeException(block.ToString());
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
            if (projectile is Projectiles.AlchemyCoin)
                return new MoveableHalt(projectile);
            return NoOp.Instance;
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
