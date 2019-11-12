using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Keese : EnemyAgent
    {
        private static readonly Random Rng = new Random();

        public override Rectangle Bounds => Alive ? new Rectangle(Location.X, Location.Y, 16, 16) : Rectangle.Empty;
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;

        private readonly Point _origin;
        private int _movementClock;
        private int _movementPauseClock;
        private Point _playerLocation;
        private Point _nextDestination;

        public Keese(Point location)
        {
            _origin = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateKeese();
            _movementPauseClock = Rng.Next(10, 120);
            Location = _origin;
        }

        public override void Halt()
        {
            // NO-OP: Flies through walls
        }

        private void ExecuteAction()
        {
            if (_movementPauseClock-- > 0)
            {
                _movementPauseClock--;
                return;
            }

            if (_movementClock > 0)
            {
                _movementClock--;
                AdvanceToDestination();
            }
            else
            {
                generateNextDestination();
                _movementClock = Rng.Next(20, 90);
                _movementPauseClock = Rng.Next(30, 60);
            }
            
        }

        private void AdvanceToDestination()
        {
            const int yBounds = 150;
            const int xBounds = 220;
            double xDiff = _playerLocation.X - _nextDestination.X;
            double yDiff = _playerLocation.Y - _nextDestination.Y;
            double magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            const float scaleDenominator = 21.3f;
            double xScale = _movementClock / scaleDenominator;
            double yScale = _movementClock / scaleDenominator;

            var normalizedY = yDiff / magnitude * yScale;
            var normalizedX = xDiff / magnitude * xScale;

            if (Location.X - (int)normalizedX > xBounds || Location.X - (int)normalizedX < 0 || Location.Y - (int)normalizedY > yBounds || Location.Y - (int)normalizedY < 0)
            {
                generateNextDestination();
            }
            else
            {
                Location.X -= (int)normalizedX;
                Location.Y -= (int)normalizedY;
            }
            
            
        }


        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        private void generateNextDestination()
        {
            const float locationScale = 1.0f;
            double xDiff = _playerLocation.X - Location.X;
            double yDiff = _playerLocation.Y - Location.Y;
            xDiff += Math.Sign(xDiff) * Rng.Next(32);
            yDiff += Math.Sign(yDiff) * Rng.Next(32);
            if (Rng.Next(3) == 0)
            {
                _nextDestination = new Point(Rng.Next(256), Rng.Next(48,200));
            }
            else
            {
                _nextDestination = new Point((int)(Location.X + xDiff * locationScale), (int)(Location.Y + yDiff * locationScale));
            }

            
        }

        public override void Update()
        {
            if (Alive && CanMove)
                ExecuteAction();
            base.Update();
        }
    }
}
