using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class KeeseAgent
    {
        private static readonly Random Rng = new Random();

        private ISprite _sprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
        private bool _isSpawning = true;
        public bool Alive { get; private set; }
        private int _flagClock;
        private uint _movementClock;
        private int _movementPauseClock;
        public Point Location;

        public KeeseAgent(Point location)
        {
            Location = location;
        }

        public void Spawn()
        {
            _flagClock = 30;
            Alive = true;
        }

        public void TakeDamage()
        {
            if (!Alive) return;
            _sprite.Hide();
            _flagClock = 50;
            _sprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
            Alive = false;
        }

        private void CheckFlags()
        {
            if (_isSpawning)
            {
                _sprite = EnemySpriteFactory.Instance.CreateKeese();
                _isSpawning = false;
            }

            if (!Alive)
            {
                _sprite.Hide();
            }
        }

        // Moves in a figure 8
        private void AdvanceLocation()
        {
            if (_movementPauseClock > 0)
            {
                _movementPauseClock--;
                return;
            }

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
                default:
                    break;
            }

            if (_movementClock == 0 || _movementClock == 40 && Rng.Next(0, 10) < 7)
            {
                _movementPauseClock = Rng.Next(20, 60);
            }

            _movementClock = (_movementClock + 1) % 80;
        }

        public void Update()
        {
            if (_flagClock-- == 0) CheckFlags();
            if (Alive && !_isSpawning) AdvanceLocation();
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(Location.ToVector2());
        }
    }
}