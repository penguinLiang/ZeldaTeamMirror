using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Enemies
{
    public abstract class EnemyAgent : IEnemy
    {
        protected abstract ISprite Sprite { get; }

        protected Point Location;
        protected int Speed = 1;
        protected const int TileSize = 8;
        protected const int AlignThreshold = 3;

        protected int Health = 1;

        protected bool Spawned;

        public List<IProjectile> Projectiles { get; set; } = new List<IProjectile>();

        public virtual bool Alive => Spawned && Health > 0;

        private ISprite _spawnSprite;
        private ISprite _deathSprite;
        private ISprite _drawnSprite;

        protected virtual bool CanMove => _spawnSprite.AnimationFinished;

        public virtual void Spawn()
        {
            Health = 1;
            Spawned = true;
            _spawnSprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
            _deathSprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
        }

        public virtual void TakeDamage(int damage)
        {
            if (Alive)
            {
                Health -= damage;
                Sprite?.PaletteShift();
                Knockback();
                SoundEffectManager.Instance.PlayEnemyHit();
                if (Health <= 0)
                {
                    SoundEffectManager.Instance.PlayEnemyDie();
                }
            }
            else
            {
                Sprite?.Hide();
            }
        }

        protected virtual void Move(Direction direction, int speed = 1)
        {
            var initialSpeed = Speed;
            Speed = speed;
            AlignMovement(direction);
            Speed = initialSpeed;
        }

        protected void AlignMovement(Direction direction)
        {
            if (direction == Direction.Down || direction == Direction.Up)
            {
                var distance = Location.X % TileSize;
                if (distance == 0)
                {
                    Location += new Point(0, direction == Direction.Down ? Speed : -Speed);
                }
                else
                {
                    Location += new Point(distance > AlignThreshold ? Speed : -Speed, 0);
                }
            }
            else
            {
                var distance = Location.Y % TileSize;
                if (distance == 0)
                {
                    Location += new Point(direction == Direction.Left ? -Speed : Speed, 0);
                }
                else
                {
                    Location += new Point(0, distance > AlignThreshold ? Speed : -Speed);
                }
            }
        }

        public virtual void Update()
        {
            if (Spawned && !_spawnSprite.AnimationFinished)
            {
                _drawnSprite = _spawnSprite;
            }
            else if (!Alive)
            {
                _drawnSprite = !_deathSprite.AnimationFinished ? _deathSprite : null;
            }
            else
            {
                _drawnSprite = Sprite;
            }

            _drawnSprite?.Update();
        }

        public virtual void Target(Point location)
        {
            // NO-OP
        }

        public virtual void Draw()
        {
            _drawnSprite?.Draw(Location.ToVector2());
        }

        public virtual bool CollidesWith(Rectangle rect)
        {
            return Alive && Bounds.Intersects(rect);
        }

        public virtual ICommand PlayerEffect(IPlayer player)
        {
            return new SpawnableDamage(player, 1);
        }

        public virtual ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public virtual ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }

        public virtual void Stun()
        {
            // NO-OP
        }

        public abstract Rectangle Bounds { get; }
        public abstract void Halt();
        protected abstract void Knockback();

        public static bool IsWithinCircularBounds(Point center, Point location, int radius)
        {
            var x = location.X - center.X;
            var y = location.Y - center.Y;
            return Math.Sqrt(x * x + y * y) < radius;
        }
    }
}
