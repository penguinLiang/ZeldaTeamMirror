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

            if (_movementClock % Rng.Next(1,10) == 0)
            {
                generateNextDestination();
            }

            if (_movementClock > 0)
            {
                _movementClock--;
                AdvanceToDestination();
            }
            else
            {
                generateNextDestination();
                _movementClock = Rng.Next(60,100);
                _movementPauseClock = Rng.Next(30,70);
            }
            
        }

        private void AdvanceToDestination()
        {
            double xDiff = _playerLocation.X - _nextDestination.X;
            double yDiff = _playerLocation.Y - _nextDestination.Y;
            double magnitude = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            double xScale = _movementClock / 24;
            double yScale = _movementClock / 24;

            var normalizedY = yDiff / magnitude * xScale;
            var normalizedX = xDiff / magnitude * yScale;
            
            Location.X -= (int)normalizedX;
            Location.Y -= (int)normalizedY;
        }


        public override void Target(Point playerLocation)
        {
            _playerLocation = playerLocation;
        }

        private void generateNextDestination()
        {
            const float locationScale = 2f;
            double xDiff = _playerLocation.X - Location.X;
            double yDiff = _playerLocation.Y - Location.Y;
            if (Rng.Next(3) == 2)
            {
                _nextDestination = new Point((int)(Location.X + Rng.Next(-100, 100)), (int)(Location.Y + Rng.Next(-100, 100)));
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
