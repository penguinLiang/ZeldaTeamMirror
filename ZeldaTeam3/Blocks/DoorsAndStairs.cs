using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Blocks
{
    class DoorsAndStairs : ICollideable, IDrawable, IActivatable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; private set; }

        private readonly HashSet<BlockType> _leftRightDoors = new HashSet<BlockType>
        {
            BlockType.DoorLockedRight,
            BlockType.DoorLockedLeft,
            BlockType.BombableWallLeft,
            BlockType.BombableWallRight,
            BlockType.DoorSpecialLeft2_1,
            BlockType.DoorSpecialRight3_1,
            BlockType.DoorRight,
            BlockType.DoorLeft,
        };

        private readonly HashSet<BlockType> _upDownDoors = new HashSet<BlockType>
        {
            BlockType.DoorUp,
            BlockType.DoorDown,
            BlockType.DoorSpecialUp1_1,
            BlockType.DoorLockedUp,
            BlockType.DoorLockedDown,
            BlockType.BombableWallTop,
            BlockType.BombableWallBottom,
        };

        private readonly HashSet<BlockType> _allStairSet = new HashSet<BlockType>
        {
            BlockType.DungeonStair,
            BlockType.BasementStair
        };

        private readonly Vector2 _drawLocation;

        public DoorsAndStairs(Point location, BlockType block)
        {
            if (_leftRightDoors.Contains(block))
            {
                Bounds = new Rectangle(location, new Point(32, 48));
                _drawLocation = new Vector2(location.X, location.Y + 8);
            }
            else if (_upDownDoors.Contains(block))
            {
                Bounds = new Rectangle(location, new Point(32, 32));
                _drawLocation = location.ToVector2();
            }
            else if (_allStairSet.Contains(block))
            {
                Bounds = new Rectangle(location, new Point(16, 16));
                _drawLocation = location.ToVector2();
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Expected Door or Stair, got {block}");
            }

            _sprite = BlockTypeSprite.Sprite(block);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new LinkKnockback(player);
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
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_drawLocation);
        }

        public void Activate()
        {
            return;
        }
    }
}
