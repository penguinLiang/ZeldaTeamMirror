using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.Projectiles;

namespace Zelda.Player
{
    internal class PlayerProjectileAgent : IDrawable
    {
        public bool UsingSecondaryItem { get; private set; }
        public Secondary Item { get; private set; }

        public List<IProjectile> Projectiles { get; }

        private readonly IPlayer _player;

        public PlayerProjectileAgent(IPlayer player)
        {
            _player = player;
            Projectiles = new List<IProjectile>();
        }

        public void FireSwordBeam(Direction facing, Point location, Primary swordLevel)
        {
            switch (facing)
            {
                case Direction.Up:
                    location.Y -= 12;
                    break;
                case Direction.Down:
                    location.Y += 11;
                    break;
                case Direction.Left:
                    location.X -= 11;
                    break;
                case Direction.Right:
                    location.X += 11;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (swordLevel)
            {
                case Primary.Sword:
                    Projectiles.Add(new SwordBeam(location, facing, 1));
                    break;
                case Primary.WhiteSword:
                    Projectiles.Add(new SwordBeam(location, facing, 2));
                    break;
                case Primary.MagicalSword:
                    Projectiles.Add(new SwordBeam(location, facing, 4));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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

            UsingSecondaryItem = true;
            var inv = _player.Inventory;
            switch (Item)
            {
                case Secondary.Bow when inv.ArrowLevel != Secondary.None && inv.BowLevel != Secondary.None && inv.TryRemoveRupee():
                    Projectiles.Add(new Arrow(location, facing));
                    break;
                case Secondary.Boomerang when inv.TryRemoveBoomerang():
                    Projectiles.Add(new PlayerBoomerang(_player, new Point(location.X + 4, location.Y + 4), facing));
                    break;
                case Secondary.Bomb when inv.TryRemoveBomb():
                    Projectiles.Add(new Bomb(location));
                    break;
                case Secondary.Coins when inv.TryRemoveCoins():
                    // TO-DO: Integrate class
                    break;
                case Secondary.ATWBoomerang when inv.TryRemoveATWBoomerang():
                    Projectiles.Add(new ATWBoomerang(_player, facing));
                    break;
                case Secondary.BombLauncher when inv.TryRemoveBomb():
                    Projectiles.Add(new LaunchedBomb(location, facing));
                    break;
                default:
                    UsingSecondaryItem = false;
                    break;
            }

        }

        public void AssignSecondaryItem(Secondary item)
        {
            Item = item;
        }

        public void Update()
        {
            UsingSecondaryItem = false;
        }

        public void Draw()
        {
            // NO-OP
        }
    }
}
