using System;
using Microsoft.Xna.Framework;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class Bomb : IProjectile
    {
        private const int FramesToExplosion = 100;
        private const int FramesToDisappear = 160;
        private const int NumberOfOuterExplosionSprites = 3;

        private readonly Vector2 _location;
        private readonly Vector2[] _outerExplosionSpriteLocations = new Vector2[NumberOfOuterExplosionSprites];
        private ISprite _sprite;
        private int _framesDelayed;
        public Rectangle Bounds { get; private set; }
        public bool Halted { get; set; }

        public Bomb(Point location)
        {
            _location = location.ToVector2();
            _sprite = ProjectileSpriteFactory.Instance.CreateBomb();
            SoundEffectManager.Instance.PlayBombDrop();
            Halted = false;
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
            
            if (_framesDelayed == FramesToExplosion)
            {
                SoundEffectManager.Instance.PlayBombExplode();
                _sprite = ProjectileSpriteFactory.Instance.CreateBombExplosion();
                SetExplosionSpriteLocations();
                Bounds  = new Rectangle((int)_location.X - 16 ,(int) _location.Y - 16, 48, 48);
            }
            else if (_framesDelayed > FramesToExplosion && _framesDelayed < FramesToDisappear)
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
            return Commands.NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new Commands.SpawnableDamage(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return Commands.NoOp.Instance;
        }

        public void Knockback()
        {
            //no op
        }

        public void Halt()
        {
            //no op
        }

        public void Draw()
        {
            _sprite.Draw(_location);

            if (_framesDelayed < FramesToExplosion) return;
            foreach (var spriteLocation in _outerExplosionSpriteLocations)
            {
                _sprite.Draw( spriteLocation);
            }
        }
    }
}
