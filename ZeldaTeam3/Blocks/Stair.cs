using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    class Stair : ICollideable, IDrawable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; private set; }

        private readonly Vector2 _drawLocation;

        public Stair(Point location, BlockType block)
        { 
                Bounds = new Rectangle(location, new Point(16, 16));
                _drawLocation = location.ToVector2();

            _sprite = BlockTypeSprite.Sprite(block);
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
    }
}
