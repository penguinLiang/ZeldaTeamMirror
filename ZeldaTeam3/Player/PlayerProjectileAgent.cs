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

        private void GenerateBowFireballs(Direction facing, Point location)
        {
            switch (facing)
            {
                case Direction.Up:
                    Projectiles.Add(new PlayerFireball(location, new Vector2(-1, -2)));
                    Projectiles.Add(new PlayerFireball(location, new Vector2(1, -2)));
                    break;
                case Direction.Left:
                    Projectiles.Add(new PlayerFireball(location, new Vector2(-2, -1)));
                    Projectiles.Add(new PlayerFireball(location, new Vector2(-2, 1)));
                    break;
                case Direction.Right:
                    Projectiles.Add(new PlayerFireball(location, new Vector2(2, -1)));
                    Projectiles.Add(new PlayerFireball(location, new Vector2(2, 1)));
                    break;
                case Direction.Down:
                    Projectiles.Add(new PlayerFireball(location, new Vector2(-1, 2)));
                    Projectiles.Add(new PlayerFireball(location, new Vector2(1, 2)));
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
                case Secondary.Bow when inv.ArrowLevel != Secondary.None && inv.TryRemoveRupee():
                    if (inv.ArrowLevel == Secondary.Arrow)
                        Projectiles.Add(new Arrow(location, facing));
                    else
                        Projectiles.Add(new SilverArrow(location, facing));
                    break;
                case Secondary.FireBow when inv.ArrowLevel != Secondary.None && inv.TryRemoveRupee():
                    if (inv.ArrowLevel == Secondary.Arrow)
                        Projectiles.Add(new Arrow(location, facing));
                    else
                        Projectiles.Add(new SilverArrow(location, facing));
                    GenerateBowFireballs(facing, location);
                    break;
                case Secondary.Boomerang when inv.TryRemoveBoomerang():
                    Projectiles.Add(new PlayerBoomerang(_player, new Point(location.X, location.Y), facing));
                    break;
                case Secondary.Bomb when inv.TryRemoveBomb():
                    Projectiles.Add(new Bomb(location));
                    break;
                case Secondary.Coins when inv.TryRemoveCoins():
                    Projectiles.Add(new AlchemyCoin(location, facing, _player.Inventory, true));
                    Projectiles.Add(new AlchemyCoin(location, facing, _player.Inventory, false));
                    break;
                case Secondary.ATWBoomerang when inv.TryRemoveATWBoomerang():
                    Projectiles.Add(new ATWBoomerang(_player, facing));
                    break;
                case Secondary.BombLauncher when inv.TryRemoveBomb():
                    Projectiles.Add(new LaunchedBomb(location, facing));
                    break;
                case Secondary.ExtraSlot1:
                    UseExtraItem(inv.RemoveExtraItem1(), location, facing);
                    break;
                case Secondary.ExtraSlot2:
                    UseExtraItem(inv.RemoveExtraItem2(), location, facing);
                    break;
                // Unconditional cases must be handled
                case Secondary.Bow:
                case Secondary.FireBow:
                case Secondary.Boomerang:
                case Secondary.Bomb:
                case Secondary.Coins:
                case Secondary.ATWBoomerang:
                case Secondary.BombLauncher:
                case Secondary.None:
                case Secondary.Arrow:
                case Secondary.SilverArrow:
                case Secondary.LaserBeam:
                case Secondary.Bait:
                    UsingSecondaryItem = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(Item.ToString());
            }

        }

        private void UseExtraItem(Items.Secondary extraItem, Point location, Direction facing)
        {
            switch (extraItem)
            {
                case Secondary.LaserBeam:
                    Projectiles.Add(new LaserBeam(location, facing));
                    break;
                case Secondary.Clock:

                    UsingSecondaryItem = false;
                    break;
                case Secondary.Star:
                    _player.MakeInvulnerable();
                    UsingSecondaryItem = false;
                    break;
                case Secondary.Bait:
                    Projectiles.Add(new Bait(new Point(location.X + 4, location.Y)));
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
