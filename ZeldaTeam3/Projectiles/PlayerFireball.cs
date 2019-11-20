using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Projectiles
{
    internal class PlayerFireball : IProjectile
    {
        private const int FramesToDisappear = 140;

        private Vector2 _location;
        private readonly Vector2 _velocity;
        private readonly ISprite _sprite = ProjectileSpriteFactory.Instance.CreateOldManFireball();

        private int _framesDelayed;

        public Rectangle Bounds => new Rectangle((int)_location.X + 4, (int)_location.Y + 4, 8, 8);
        public bool Halted { get; set; } 

        public PlayerFireball(Point location, Vector2 velocity)
        {
            _location = location.ToVector2();
            _velocity = velocity;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.Size != Point.Zero && Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            Halt();
            _sprite.Hide();
            return new SpawnableDamage(enemy, 1);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt() {
            Halted = true;
        }

        public void Reflect(Direction direction)
        {
            //NO-OP
        }

        public void Update()
        {
            _location = Vector2.Add(_location, _velocity);
            if (_framesDelayed++ >= FramesToDisappear)
            {
                _sprite.Hide();
                Halt();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
