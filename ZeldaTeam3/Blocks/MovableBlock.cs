﻿using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;

namespace Zelda.Blocks
{
    internal class MovableBlock : ICollideable, IDrawable, ITransitionResetable
    {
        private readonly ISprite _sprite = new AlphaPassMask(BlockSpriteFactory.Instance.CreateSolidBlock(), true);
        public Rectangle Bounds => new Rectangle(_location.X, _location.Y, 16, 16);

        private readonly Point _origin;
        private Point _location;
        private Direction _pushDirection;
        private int _distanceMoved;

        private bool _unmoved;
        private bool _moving;
        public bool Locked { get; private set; }

        public MovableBlock(Point location)
        {
            _origin = location;
            Reset();
        } 

        public bool CollidesWith(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        private bool TrySetBlockDirection(Rectangle playerBounds)
        {
            var overlap = Rectangle.Intersect(Bounds, playerBounds);
            if (overlap.Width > overlap.Height)
            {
                _pushDirection = Bounds.Y < playerBounds.Y ? Direction.Up : Direction.Down;
            }
            else if (overlap.Width < overlap.Height)
            {
                _pushDirection = Bounds.X < playerBounds.X ? Direction.Left : Direction.Right;
            }
            else
            {
                return false;
            }
            return true;
        }

        public ICommand PlayerEffect(IPlayer player)
        {
            // ReSharper disable once InvertIf (cleaner as-is)
            if (_unmoved && TrySetBlockDirection(player.BodyCollision.Bounds))
            {
                _moving = true;
                _unmoved = false;
            }
            return new MoveableHalt(player);
        }

        public ICommand EnemyEffect(IEnemy enemy)
        {
            return new MoveableHalt(enemy);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Update()
        {
            if (!_moving || Locked) return;
            
            switch (_pushDirection)
            {
                case Direction.Up:
                    _location = new Point(_location.X, _location.Y - 1);
                    break;
                case Direction.Down:
                    _location = new Point(_location.X, _location.Y + 1);
                    break;
                case Direction.Left:
                    _location = new Point(_location.X - 1, _location.Y);
                    break;
                case Direction.Right:
                    _location = new Point(_location.X + 1, _location.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (++_distanceMoved < 16) return;

            if (_moving)
                SoundEffectManager.Instance.PlayPuzzleSolved();
            _moving = false;
            Locked = true;
        }

        public void Draw()
        {
            _sprite.Draw(_location.ToVector2());
        }

        public void Reset()
        {
            _location = _origin;
            _distanceMoved = 0;
            _unmoved = true;
            _moving = false;
            Locked = false;
        }
    }
}
