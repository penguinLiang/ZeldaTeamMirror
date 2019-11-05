using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class Stair : ICollideable, IDrawable
    {
        private readonly ISprite _sprite;
        public Rectangle Bounds { get; }
        private readonly DungeonManager _dungeonManager;
        private readonly BlockType _block;
        private readonly Vector2 _drawLocation;

        public Stair(DungeonManager dungeon, Point location, BlockType block)
        {
            Bounds = new Rectangle(location, new Point(16, 16));
            _drawLocation = location.ToVector2();
            _dungeonManager = dungeon;
            _block = block;
            _sprite = BlockTypeSprite.Sprite(block);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_block)
            {
                case BlockType.DungeonStair:
                    return new SceneTransition(_dungeonManager, 1, 1);
                case BlockType.StairSpecialUp1_1:
                    return new SceneTransition(_dungeonManager, 0, 1);
                default:
                    return new MoveableHalt(player);
            }
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
            _sprite?.Draw(_drawLocation);
        }
    }
}
