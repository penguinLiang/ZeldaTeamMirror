using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Player
{
    internal class SecondaryItemAgent : IDrawable
    {
        // 600/60 frames = ~10s
        private const int DrawableExpiration = 600;

        private readonly List<IDrawable> _drawables = new List<IDrawable>();
        private readonly List<int> _drawableExpirations = new List<int>();

        private Items.Secondary _item;

        public void UseSecondaryItem(Direction facing, Point location)
        {
            switch (facing)
            {
                case Direction.Up:
                    location.Y -= 16;
                    break;
                case Direction.Down:
                    location.Y += 16;
                    break;
                case Direction.Left:
                    location.X -= 16;
                    break;
                case Direction.Right:
                    location.X += 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (_item)
            {
                case Items.Secondary.Bow:
                    _drawables.Add(new Projectiles.Arrow(location, facing));
                    break;
                case Items.Secondary.Boomerang:
                    location.X += 4;
                    location.Y += 4;
                    _drawables.Add(new Projectiles.PlayerBoomerang(location, facing));
                    break;
                case Items.Secondary.Bomb:
                    _drawables.Add(new Projectiles.Bomb(location));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _drawableExpirations.Add(0);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            _item = item;
        }

        public void Update()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Update();
            }

            for (var i = 0; i < _drawableExpirations.Count; i++)
            {
                if (_drawableExpirations[i]++ != DrawableExpiration) return;
                _drawableExpirations.RemoveAt(i);
                _drawables.RemoveAt(i);
            }
        }

        public void Draw()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Draw();
            }
        }
    }
}
