using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;


namespace Zelda.Blocks
{
    internal class MovableBlock : ICollideable, IDrawable
    {
        private readonly ISprite _sprite = BlockSpriteFactory.Instance.CreateSolidBlock();
        public Point Location { get; private set; }
        public Rectangle Bounds { get; private set; }

        private Direction _pushDirection;
        private int _distanceMoved;

        private bool _unmoved = true;
        private bool _moving;

        public MovableBlock(Point location)
        {
            Location = location;
            Bounds = new Rectangle(location.X, location.Y, 16, 16);
        }

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        private bool TrySetBlockDirection(Rectangle playerBounds)
        {
            Rectangle overlap = Rectangle.Intersect(Bounds, playerBounds);
            if (overlap.Width > overlap.Height)
            {
                if (Bounds.Y < playerBounds.Y)
                {
                    _pushDirection = Direction.Up;
                }
                else
                {
                    _pushDirection = Direction.Down;
                }
            }
            else if (overlap.Width < overlap.Height)
            {
                if (Bounds.X < playerBounds.X)
                {
                    _pushDirection = Direction.Left;
                }
                else
                {
                    _pushDirection = Direction.Right;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            if (_unmoved && TrySetBlockDirection(player.Bounds))
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
                        Location = new Point(Location.X, Location.Y - 1);
                        Bounds = new Rectangle(Location.X, Location.Y, 16, 16);
                        break;
                    case Direction.Down:
                        Location = new Point(Location.X, Location.Y + 1);
                        Bounds = new Rectangle(Location.X, Location.Y, 16, 16);
                        break;
                    case Direction.Left:
                        Location = new Point(Location.X - 1, Location.Y);
                        Bounds = new Rectangle(Location.X, Location.Y, 16, 16);
                        break;
                    case Direction.Right:
                        Location = new Point(Location.X + 1, Location.Y);
                        Bounds = new Rectangle(Location.X, Location.Y, 16, 16);
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
            _sprite.Draw(Location.ToVector2());
        }
    }
}
