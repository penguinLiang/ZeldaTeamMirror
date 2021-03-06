﻿using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    internal class SilverArrow :  IProjectile
    {
        private const int FramesToDisappear = 140;
        private const int ArrowSpeed = 4;

        private readonly ISprite _sprite;
        private readonly BasicProjectileStateMachine _arrowStateMachine;
        public Rectangle Bounds => _arrowStateMachine.Bounds;

        private int _framesDelayed;
        public bool Halted { get; set; }

        public SilverArrow(Point location, Direction direction)
        {
            SoundEffectManager.Instance.PlayArrowShoot();
            switch (direction)
            {
                case Direction.Up:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSilverArrowUp();
                    break;
                case Direction.Down:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSilverArrowDown();
                    break;
                case Direction.Left:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSilverArrowLeft();
                    break;
                case Direction.Right:
                    _sprite = ProjectileSpriteFactory.Instance.CreateSilverArrowRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _arrowStateMachine = new BasicProjectileStateMachine(location, direction, ArrowSpeed);
            Halted = false;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return _arrowStateMachine.CollidesWith(rectangle);
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            return NoOp.Instance;
        }


        public ICommand EnemyEffect(IEnemy enemy)
        {
            _sprite.Hide();
            _arrowStateMachine.ClearBounds();
            Halt();
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
            _arrowStateMachine.Update();
            if (_framesDelayed++ == FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_arrowStateMachine.Location.ToVector2());
        }
    }
}
