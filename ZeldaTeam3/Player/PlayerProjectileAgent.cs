using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Projectiles;

namespace Zelda.Player
{
    internal class PlayerProjectileAgent : IDrawable
    {
        public bool UsingSecondaryItem;

        public Items.Secondary Item;

        public List<IProjectile> Projectiles { get; set; }

        public PlayerProjectileAgent()
        {
            UsingSecondaryItem = false;
            Projectiles = new List<IProjectile>();
        }

        public void FireSwordBeam(Direction facing, Point location, Items.Primary swordLevel)
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
                case Items.Primary.Sword:
                    Projectiles.Add(new SwordBeam(location, facing, 1));
                    break;
                case Items.Primary.WhiteSword:
                    Projectiles.Add(new SwordBeam(location, facing, 2));
                    break;
                case Items.Primary.MagicalSword:
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

            switch (Item)
            {
                case Items.Secondary.Bow:
                    //check rupee count, if you have more than 1 rupee, you can fire the bow, else no op
                    //subtract from rupee count
                    //Should probably also check that the player actually has the bow
                    var arrow = new Arrow(location, facing);
                    Projectiles.Add(arrow);
                    break;
                case Items.Secondary.Boomerang:
                    //should probably check that the player actually has the boomerang
                    location.X += 4;
                    location.Y += 4;
                    var playerBoomerang = new PlayerBoomerang(location, facing);
                    Projectiles.Add(playerBoomerang);
                    break;
                case Items.Secondary.Bomb:
                    //Check the player has bombs first
                    var bomb = new Bomb(location);
                    Projectiles.Add(bomb);
                    //subtract from bomb count
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UsingSecondaryItem = true;
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            Item = item;
        }

        public void Update()
        {
            UsingSecondaryItem = false;
        }

        public void Draw()
        {
        }
    }
}
