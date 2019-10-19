using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Keese : EnemyAgent
    {
        private static readonly Random Rng = new Random();

        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateKeese();

        private readonly Point _origin;
        private uint _movementClock;
        private int _movementPauseClock;

        public Keese(Point location)
        {
            _origin = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            Location = _origin;
        }

        public override void Halt()
        {
            // NO-OP: Flies through walls
        }

        // Moves in a figure 8
        private void AdvanceLocation()
        {
            if (_movementPauseClock > 0)
            {
                _movementPauseClock--;
                return;
            }

            // ReSharper disable once SwitchStatementMissingSomeCases (Value reset after 80)
            switch (_movementClock / 10)
            {
                case 0:
                case 2:
                    Location.X += 2;
                    break;
                case 1: 
                    Location.X += 2;
                    Location.Y += 2;
                    break;
                case 3:
                case 7:
                    Location.Y -= 2;
                    break;
                case 4:
                case 6:
                    Location.X -= 2;
                    break;
                case 5:
                    Location.X -= 2;
                    Location.Y += 2;
                    break;
            }

            if (_movementClock == 0 || _movementClock == 40 && Rng.Next(0, 10) < 7)
            {
                _movementPauseClock = Rng.Next(20, 60);
            }

            _movementClock = (_movementClock + 1) % 80;
        }

        public override void Update()
        {
            if (Alive && CanMove)
                AdvanceLocation();
            base.Update();
        }
    }
}
