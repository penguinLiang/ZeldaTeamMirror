using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class LaunchedBombExplosion : IProjectile
    {
        private const int FramesToDisappear = 60;
        private const int NumberOfOuterExplosionSprites = 3;

        private readonly Vector2 _location;
        private readonly Vector2[] _outerExplosionSpriteLocations = new Vector2[NumberOfOuterExplosionSprites];
        private readonly ISprite _sprite = ProjectileSpriteFactory.Instance.CreateBombExplosion();
        private int _framesDelayed;
        public Rectangle Bounds { get; }
        public bool Halted { get; set; }

        public LaunchedBombExplosion(Point location)
        {
            _location = location.ToVector2();
            Bounds = new Rectangle(location.X - 16, location.Y - 16, 48, 48);
            SoundEffectManager.Instance.PlayBombExplode();
            SetExplosionSpriteLocations();
        }

        private void SetExplosionSpriteLocations()
        {
            if (_framesDelayed % 2 == 0)
            {
                _outerExplosionSpriteLocations[0] = new Vector2(_location.X - 8, _location.Y - 16);
                _outerExplosionSpriteLocations[1] = new Vector2(_location.X + 16, _location.Y);
                _outerExplosionSpriteLocations[2] = new Vector2(_location.X + 8, _location.Y + 16);
            }
            else
            {
                _outerExplosionSpriteLocations[0] = new Vector2(_location.X + 8, _location.Y - 16);
                _outerExplosionSpriteLocations[1] = new Vector2(_location.X - 16, _location.Y);
                _outerExplosionSpriteLocations[2] = new Vector2(_location.X - 8, _location.Y + 16);
            }
        }

        public void Update()
        {
            _sprite.Update();
            _framesDelayed++;
            
            if (_framesDelayed < FramesToDisappear)
            {
                SetExplosionSpriteLocations();
            }
            else if (_framesDelayed == FramesToDisappear)
            {
                Halted = true;
                _sprite.Hide();
            }
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
            return new SpawnableDamage(enemy, 4);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            //NO-OP
        }

        public void Draw()
        {
            _sprite.Draw(_location);
            foreach (var spriteLocation in _outerExplosionSpriteLocations)
            {
                _sprite.Draw( spriteLocation);
            }
        }
    }
}
