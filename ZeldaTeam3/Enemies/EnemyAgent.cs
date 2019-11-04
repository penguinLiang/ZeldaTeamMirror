using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;

namespace Zelda.Enemies
{
    public abstract class EnemyAgent : IEnemy
    {
        protected abstract ISprite Sprite { get; }

        protected Point Location;
        protected const int Velocity = 1;

        protected int Health = 1;

        protected bool Spawned;

       public abstract List<IProjectile> Projectiles { get; set; }
        //Most enemies have no projectiles

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

        public virtual void TakeDamage()
        {
            if (Alive)
            {
                Health--;
                Sprite?.PaletteShift();
            }
            else
            {
                Sprite?.Hide();
            }
        }

        protected virtual void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Location.Y -= Velocity;
                    break;
                case Direction.Down:
                    Location.Y += Velocity;
                    break;
                case Direction.Left:
                    Location.X -= Velocity;
                    break;
                case Direction.Right:
                    Location.X += Velocity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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

        public virtual void Draw()
        {
            _drawnSprite?.Draw(Location.ToVector2());
        }

        public virtual bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public virtual ICommand PlayerEffect(IPlayer player)
        {
            return new SpawnableDamage(player);
        }

        public virtual ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public virtual ICommand ProjectileEffect(IProjectile projectile)
        {
            return new MoveableHalt(projectile);
        }

        public virtual void Knockback()
        {
            // NO-OP: Most enemies are 1-hit kills
        }

        public virtual void Stun()
        {
            // NO-OP: Most enemies are 1-hit kills
        }

        public abstract Rectangle Bounds { get; }
        public abstract void Halt();
    }
}
