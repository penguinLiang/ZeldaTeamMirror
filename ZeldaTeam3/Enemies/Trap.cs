using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Zelda.Enemies
{
    public class Trap : EnemyAgent
    {
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        private readonly Point _origin;

        private Point _lastLocation;
        private Direction _moving;
        private bool _halted;
        private int _distance;
        public override List<IProjectile> Projectiles { get; set; }


        public Trap(Point location)
        {
            _origin = location;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateTrap();
            Location = _origin;
            _moving = Direction.Right;
        }

        public override void TakeDamage()
        {
            // NO-OP: No damage
        }

        public override void Halt()
        {
            _halted = true;
        }

        public override void Update()
        {
            base.Update();
            if (_halted)
            {
                Location = _lastLocation;
                _distance = 0;
                _halted = false;
                _moving = DirectionUtility.RotateClockwise(_moving);
                return;
            }

            if (!CanMove) return;
            _lastLocation = Location;
            Move(_moving);

            if (_distance++ != 30) return;
            _distance = 0;
            _moving = DirectionUtility.RotateClockwise(_moving);
        }
    }
}
