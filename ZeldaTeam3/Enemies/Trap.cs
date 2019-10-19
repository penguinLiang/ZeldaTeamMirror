using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Trap : EnemyAgent
    {
        public override Rectangle Bounds => new Rectangle(Location.X, Location.Y, 16, 16);
        protected override ISprite Sprite { get; } = EnemySpriteFactory.Instance.CreateTrap();
        public override bool Alive => true;

        private Point _lastLocation;
        private Direction _moving = Direction.Right;
        private bool _halted;
        private int _distance;

        public Trap(Point location)
        {
            Location = location;
        }

        public override void Halt()
        {
            _halted = true;
        }

        public override void Update()
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
            Move(_moving);

            if (_distance++ != 30) return;
            _distance = 0;
            _moving = DirectionUtility.RotateClockwise(_moving);
        }

        public override void Draw()
        {
            Sprite.Draw(Location.ToVector2());
        }
    }
}
