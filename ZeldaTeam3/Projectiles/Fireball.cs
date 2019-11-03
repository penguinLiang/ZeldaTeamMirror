using System;
using Microsoft.Xna.Framework;

namespace Zelda.Projectiles
{
    internal class Fireball : IProjectile, IDrawable
    {
        private const int FramesToDisappear = 140;

        private Vector2 _location;
        private Vector2 _velocity;
        private readonly ISprite _sprite;

        private int _framesDelayed;

        public Rectangle Bounds { get; private set; }
        private ProjectileManager _projectileManager;

        public Fireball(Point location, Vector2 velocity, bool fromAquamentus)
        {
            _location = location.ToVector2();
            Bounds = new Rectangle(location.X + 4, location.Y + 4, 8, 8);
            _velocity = velocity;
            _framesDelayed = 0;
            if (fromAquamentus)
            {
                _sprite = ProjectileSpriteFactory.Instance.CreateAquamentusFireball();
            }
            else
            {
                _sprite = ProjectileSpriteFactory.Instance.CreateOldManFireball();
            }
            _projectileManager = new ProjectileManager();
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return Bounds.Intersects(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return new Commands.SpawnableDamage(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return Commands.NoOp.Instance;
        }

        public ICommand ProjectileEffect(IHaltable projectile)
        {
            return Commands.NoOp.Instance;
        }

        public void Halt() {
            RemoveProjectile(this);
        }

        public void Knockback()
        {
            //no op
        }

        public void AddProjectile(IProjectile projectile) {
            _projectileManager.AddProjectile(projectile);
        }

        public void RemoveProjectile(IProjectile projectile) {
            _projectileManager.AddProjectile(projectile);
        }

        public void Update()
        {
            _location = Vector2.Add(_location, _velocity);
            if (_framesDelayed++ >= FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
