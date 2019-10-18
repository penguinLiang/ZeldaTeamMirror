using System;
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

        public virtual bool Alive => Spawned && Health > 0;

        private readonly ISprite _spawnSprite = EnemySpriteFactory.Instance.CreateSpawnExplosion();
        private readonly ISprite _deathSprite = EnemySpriteFactory.Instance.CreateDeathSparkle();
        private ISprite _drawnSprite;

        protected virtual bool CanMove => _spawnSprite.AnimationFinished;

        public virtual void Spawn()
        {
            Spawned = true;
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

        protected void Move(Direction direction)
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

        public virtual ICommand ProjectileEffect(IHaltable projectile)
        {
            return new MoveableHalt(projectile);
        }

        public abstract Rectangle Bounds { get; }
        public abstract void Knockback();
        public abstract void Halt();
        public abstract void Stun();
    }
}
