using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class Room2_1Block : ICollideable, IDrawable
    {
        private MovableBlock _internalBlock;
        public Rectangle Bounds => _internalBlock.Bounds;

        private readonly Room _room;
        private readonly Point _location;

        public Room2_1Block(Room room, Point location)
        {
            _location = location;
            _room = room;
            _internalBlock = new MovableBlock(location);
        } 

        public void Reset()
        {
            _internalBlock = new MovableBlock(_location);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return _internalBlock.CollidesWith(rect);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (_room.SomeEnemiesAlive)
            {
                return new MoveableHalt(player);
            }

            if (!_internalBlock.Moving)
            {
                _room.Doors[Direction.Left].Activate();
            }
            return _internalBlock.PlayerEffect(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return _internalBlock.EnemyEffect(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return _internalBlock.ProjectileEffect(projectile);
        }

        public void Update()
        {
            _internalBlock.Update();
        }

        public void Draw()
        {
            _internalBlock.Draw();
        }
    }
}
