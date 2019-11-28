using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Player;
using Zelda.SoundEffects;

namespace Zelda.Projectiles
{
    internal class AlchemyCoin : IProjectile
    {
        private const int FramesToDisappear = 300;
        private const int CollisionsToDisappear = 4;
        private const int SpeedAlongAxis = 1;

        private readonly ISprite _sprite = ProjectileSpriteFactory.Instance.CreateAlchemyCoin();
        private readonly Inventory _inventory;

        private Vector2 _location;
        private int _velocityX;
        private int _velocityY;
        private int _framesDelayed;
        private int _collisions;

        public Rectangle Bounds => new Rectangle((int)_location.X + 2, (int)_location.Y + 2, 11, 11);
        public bool Halted { get; set; } 

        public AlchemyCoin(Point location, Direction playerFacing, Inventory inventory, bool goRelativeRight)
        {
            _location = location.ToVector2();
            _inventory = inventory;
            switch (playerFacing)
            {
                case Direction.Up:
                    _velocityX = goRelativeRight ? SpeedAlongAxis : -SpeedAlongAxis;
                    _velocityY = -SpeedAlongAxis;
                    break;
                case Direction.Left:
                    _velocityX = -SpeedAlongAxis;
                    _velocityY = goRelativeRight ? -SpeedAlongAxis : SpeedAlongAxis;
                    break;
                case Direction.Right:
                    _velocityX = SpeedAlongAxis;
                    _velocityY = goRelativeRight ? SpeedAlongAxis : -SpeedAlongAxis;
                    break;
                case Direction.Down:
                    _velocityX = goRelativeRight ? -SpeedAlongAxis : SpeedAlongAxis;
                    _velocityY = SpeedAlongAxis;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
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
            _inventory.Add1Rupee();
            SoundEffectManager.Instance.PlayPickupRupee();
            return new SpawnableDamage(enemy, 1);
        }

        public ICommand ProjectileEffect(IProjectile projectile)
        {
            return NoOp.Instance;
        }

        public void Halt()
        {
            // NO-OP
        }

        private void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    _velocityY = -SpeedAlongAxis;
                    break;
                case Direction.Left:
                    _velocityX = -SpeedAlongAxis;
                    break;
                case Direction.Right:
                    _velocityX = SpeedAlongAxis;
                    break;
                case Direction.Down:
                    _velocityY = SpeedAlongAxis;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        public void Reflect(List<Rectangle> orderedBounds)
        {
            bool existsTopBottomCollision = false;
            bool existsLeftRightCollision = false;
            bool existsCornerCollision = false;
            Rectangle overlap;
            Point cornerCollisionCoordinates = Point.Zero;

            foreach (var rectangle in orderedBounds)
            {
                overlap = Rectangle.Intersect(Bounds, rectangle);
                if (overlap.Size == Point.Zero) continue;

                if (!existsTopBottomCollision && overlap.Width > overlap.Height)
                {
                    existsTopBottomCollision = true;
                    if (Bounds.Y <= rectangle.Y)
                        ChangeDirection(Direction.Up);
                    else
                        ChangeDirection(Direction.Down);
                }
                else if (!existsLeftRightCollision && overlap.Width < overlap.Height)
                {
                    existsLeftRightCollision = true;
                    if (Bounds.X <= rectangle.X)
                        ChangeDirection(Direction.Left);
                    else
                        ChangeDirection(Direction.Right);
                }
                else if (overlap.Width == overlap.Height)
                {
                    existsCornerCollision = true;
                    cornerCollisionCoordinates = rectangle.Location;
                }

                if (existsTopBottomCollision && existsLeftRightCollision && !existsCornerCollision) break;
            }

            if (existsCornerCollision && !existsTopBottomCollision && !existsLeftRightCollision)
            {
                if (Bounds.Y <= cornerCollisionCoordinates.Y)
                    ChangeDirection(Direction.Up);
                else
                    ChangeDirection(Direction.Down);

                if (Bounds.X <= cornerCollisionCoordinates.X)
                    ChangeDirection(Direction.Left);
                else
                    ChangeDirection(Direction.Right);
            }

            if (existsCornerCollision || existsLeftRightCollision || existsTopBottomCollision)
            {
                _collisions++;
                SoundEffectManager.Instance.PlayDeflect();
            }
        }

        public void Update()
        {
            if (_framesDelayed++ == FramesToDisappear || _collisions >= CollisionsToDisappear)
            {
                _sprite.Hide();
                _inventory.AddCoin();
                Halted = true;
            }
            _location.X += _velocityX;
            _location.Y += _velocityY;
            _sprite.Update();
        }

        public void Draw()
        {
            _sprite.Draw(_location);
        }
    }
}
