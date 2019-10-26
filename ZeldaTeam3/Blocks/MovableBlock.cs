using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;

namespace Zelda.Blocks
{
    internal class MovableBlock : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateSolidBlock();
        public Rectangle Bounds { get; private set; }

        private Point _location;
        private Direction _pushDirection;
        private int _distanceMoved;
        private BlockType _block;
        private Room _room;

        private bool _unmoved = true;
        private bool _moving;

        public MovableBlock(Room room, BlockType block, Point location)
        {
            _location = location;
            _block = block;
            _room = room;
            Bounds = new Rectangle(location.X, location.Y, 16, 16);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        private bool TrySetBlockDirection(Rectangle playerBounds)
        {
            var overlap = Rectangle.Intersect(Bounds, playerBounds);
            if (overlap.Width > overlap.Height)
            {
                _pushDirection = Bounds.Y < playerBounds.Y ? Direction.Up : Direction.Down;
            }
            else if (overlap.Width < overlap.Height)
            {
                _pushDirection = Bounds.X < playerBounds.X ? Direction.Left : Direction.Right;
            }
            else
            {
                return false;
            }
            return true;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (_block != BlockType.Block2_1 && _unmoved && TrySetBlockDirection(player.BodyCollision.Bounds))
            {
                _moving = true;
                _unmoved = false;
            }
            bool _atLeastOneAlive = false;
            foreach(IEnemy _currentEnemy in _room.Enemies) 
            {
                if(_currentEnemy.Alive) 
                {
                    _atLeastOneAlive = true;
                    break;
                }
            }
            if (_block == BlockType.Block2_1 && !_atLeastOneAlive && _unmoved && TrySetBlockDirection(player.BodyCollision.Bounds)) 
            {
                _moving = true;
                _unmoved = false;
            }
            return new MoveableHalt(player);
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
            if (_moving)
            {
                switch (_pushDirection)
                {
                    case Direction.Up:
                        _location = new Point(_location.X, _location.Y - 1);
                        Bounds = new Rectangle(_location.X, _location.Y, 16, 16);
                        break;
                    case Direction.Down:
                        _location = new Point(_location.X, _location.Y + 1);
                        Bounds = new Rectangle(_location.X, _location.Y, 16, 16);
                        break;
                    case Direction.Left:
                        _location = new Point(_location.X - 1, _location.Y);
                        Bounds = new Rectangle(_location.X, _location.Y, 16, 16);
                        break;
                    case Direction.Right:
                        _location = new Point(_location.X + 1, _location.Y);
                        Bounds = new Rectangle(_location.X, _location.Y, 16, 16);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (++_distanceMoved >= 16)
                {
                    _moving = false;
                }
            }

            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_location.ToVector2());
        }
    }
}
