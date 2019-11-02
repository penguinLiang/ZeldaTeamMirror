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


        public Items.Secondary Item;

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

            switch (Item)
            {
                case Items.Secondary.Bow:
                    var Arrow = new Projectiles.Arrow(location, facing);
                    _drawables.Add(Arrow);
                    break;
                case Items.Secondary.Boomerang:
                    location.X += 4;
                    location.Y += 4;
                    var PlayerBoomerang = new Projectiles.PlayerBoomerang(location, facing);
                    _drawables.Add(PlayerBoomerang);
                    break;
                case Items.Secondary.Bomb:
                    var Bomb = new Projectiles.Bomb(location);
                    _drawables.Add(Bomb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _drawableExpirations.Add(0);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            Item = item;
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
