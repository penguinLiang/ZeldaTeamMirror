using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    public class LaunchedBomb : IProjectile
    {
        private const int FramesToDisappear = 140;
        private const int LaunchedBombSpeed = 3;

        private readonly ISprite _sprite;
        private readonly BasicProjectileStateMachine _launchedBombStateMachine;

        public Rectangle Bounds => _launchedBombStateMachine.Bounds;
        public bool Halted { get; set; }

        private int _framesDelayed;

        public LaunchedBomb(Point location, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _sprite = ProjectileSpriteFactory.Instance.CreateBombUp();
                    break;
                case Direction.Down:
                    _sprite = ProjectileSpriteFactory.Instance.CreateBombDown();
                    break;
                case Direction.Left:
                    _sprite = ProjectileSpriteFactory.Instance.CreateBombLeft();
                    break;
                case Direction.Right:
                    _sprite = ProjectileSpriteFactory.Instance.CreateBombRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _launchedBombStateMachine = new BasicProjectileStateMachine(location, direction, LaunchedBombSpeed);
            SoundEffectManager.Instance.PlayBombExplode();
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _launchedBombStateMachine.CollidesWith(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            _sprite.Hide();
            _launchedBombStateMachine.ClearBounds();
            return new SpawnableDamage(enemy, 4);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            Halted = true;
        }

        public void Update()
        {
            _launchedBombStateMachine.Update();
            if (_framesDelayed++ == FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_launchedBombStateMachine.Location.ToVector2());
        }
    }
}
