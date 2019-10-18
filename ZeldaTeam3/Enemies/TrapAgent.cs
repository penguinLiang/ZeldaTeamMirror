using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class TrapAgent
    {
        private readonly ISprite _sprite;

        public Point Location;
        private Point _lastLocation;
        private Direction _moving = Direction.Right;
        private bool _halted;
        private int _distance;

        public TrapAgent(Point location)
        {
            Location = location;
            _sprite = EnemySpriteFactory.Instance.CreateTrap();
            _sprite.Hide();
        }

        public void Spawn()
        {
            _sprite.Show();
        }

        public void Draw()
        {
            _sprite.Draw(Location.ToVector2());
        }

        public void Update()
        {
            if (_halted)
            {
                Location = _lastLocation;
                _distance = 0;
                _halted = false;
                _moving = DirectionUtility.RotateClockwise(_moving);
                return;
            }

            _lastLocation = Location;
            switch (_moving)
            {
                case Direction.Up:
                    Location.Y -= 1;
                    break;
                case Direction.Down:
                    Location.Y += 1;
                    break;
                case Direction.Left:
                    Location.X -= 1;
                    break;
                case Direction.Right:
                    Location.X += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            if (_distance == 30)
            {
                _distance = 0;
                _moving = DirectionUtility.RotateClockwise(_moving);
            }
            else
            {
                _distance++;
            }
        }

        public void Halt()
        {
            _halted = true;
        }
    }
}