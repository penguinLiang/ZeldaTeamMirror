using System;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Trap : EnemyAgent
    {
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        private readonly Point _origin;
        private readonly Rectangle _viewYBounds;
        private readonly Rectangle _viewXBounds;
        private Point _playerLocation;
        private Direction _direction;

        private int _movementTimer;
        private bool _restored;
        private const int ViewDistance = 12*16;
        private const int MovementVerticalTime = 20;
        private const int MovementHorizontalTime = 42;
        private int _spawnStun;

        public Trap(Point location)
        {
            _origin = location;
            _viewYBounds = new Rectangle(location.X - 8, location.Y - ViewDistance, 20, 16 + 2 * ViewDistance);
            _viewXBounds = new Rectangle(location.X - ViewDistance, location.Y - 8, 16 + 2 * ViewDistance, 20);
        }

        public override void Spawn()
        {
            base.Spawn();
            _spawnStun = 100;
            _restored = true;
            _sprite = EnemySpriteFactory.Instance.CreateTrap();
            Location = _origin;
        }

        public override void TakeDamage()
        {
            // NO-OP: No damage
        }

        public override void Halt()
        {
            _movementTimer = 0;
        }

        public override void Target(Point location)
        {
            _playerLocation = location;
        }

        public override void Update()
        {
            base.Update();
            if (--_spawnStun > 0) return;
            if (!_restored)
            {
                Move();
            }
            else if (IsPlayerInSight())
            {
                InitiateMovement();
            }
        }

        private void InitiateMovement()
        {
            _restored = false;
            SetMovement();
        }

        private void SetMovement()
        {
            var xDiff = _playerLocation.X - _origin.X;
            var yDiff = _playerLocation.Y - _origin.Y;

            if (_viewXBounds.Contains(_playerLocation))
            {
                _direction = xDiff >= 0 ? Direction.Right : Direction.Left;
                _movementTimer = MovementHorizontalTime;
            }
            else
            {
                _direction = yDiff >= 0 ? Direction.Down : Direction.Up;
                _movementTimer = MovementVerticalTime;
            }
        }

        private void Move()
        {
            if (_movementTimer > 0)
            {
                _movementTimer--;
                for (int scale = 2; scale > 0; scale--)
                {
                    Move(_direction);
                }
            }
            else
            {
                Move(DirectionUtility.Flip(_direction));
                _restored = Location.Equals(_origin);
            }
        }

        private bool IsPlayerInSight()
        {
            return _viewXBounds.Contains(_playerLocation) || _viewYBounds.Contains(_playerLocation);
        }
    }
}
