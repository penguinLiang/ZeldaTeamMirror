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
        private Point _lastLocation;
        private Point _playerLocation;
        private Direction _direction;
        private bool _movingBack;

        private int _movementTimer;
        private const int _viewDistance = 64;
        private const int _moveDistance = 64;

        public Trap(Point location)
        {
            _origin = location;
            _viewYBounds = new Rectangle(location.X, location.Y - _viewDistance, 16, 16 + 2 * _viewDistance);
            _viewXBounds = new Rectangle(location.X - _viewDistance, location.Y, 16 + 2 * _viewDistance, 16);
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateTrap();
            Location = _origin;
        }

        public override void TakeDamage()
        {
            // NO-OP: No damage
        }

        public override void Halt()
        {
            _movingBack = true;
        }

        public override void Target(Point location)
        {
            _playerLocation = location;
        }

        public override void Update()
        {
            base.Update();
            if (_movementTimer > 0)
            {
                _movementTimer--;
                Move();
            }
            if (IsPlayerInSight())
            {
                if (_movementTimer == 0)
                {
                    InitiateMovement();
                }
            }
        }

        private void InitiateMovement()
        {
            _movementTimer = 300;
            _movingBack = false;


        }

        private void Move()
        {

        }


        private bool IsPlayerInSight()
        {
            return _viewXBounds.Contains(_playerLocation) || _viewYBounds.Contains(_playerLocation);
        }
    }
}
