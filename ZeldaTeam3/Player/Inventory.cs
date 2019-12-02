using System;
using Zelda.Items;

namespace Zelda.Player
{
    public class Inventory
    {
        private const int MaxRupeeCountInitial = 255;
        private const int MaxBombCountInitial = 8;

        private const int MaxKeyCount = 255;

        public int MaxRupeeCount { get; private set; } = MaxRupeeCountInitial;
        public int MaxBombCount { get; private set; } = MaxBombCountInitial;
        public int RupeeMultiplier { get; private set; } = 1;

        public Primary SwordLevel { get; private set; }
        public Secondary SecondaryItem { get; private set; }
        public bool HasBoomerang { get; private set; }
        public int BombCount { get; private set; }
        public Secondary BowLevel { get; private set; }
        public Secondary ArrowLevel { get; private set; }
        public int Coins { get; private set; }
        public bool HasATWBoomerang { get; private set; }
        public bool HasBombLauncher { get; private set; }
        public Secondary ExtraItem1 { get; private set; }
        public Secondary ExtraItem2 { get; private set; }
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; set; } = 2000000;
        public int KeyCount { get; private set; } = 80;

        // For non-invasive backwards compatibility purposes only
        public bool HasArrow => true;
        public bool HasBow => BowLevel != Secondary.None;

        public Inventory() {
            BombCount = MaxBombCountInitial / 2;
        }

        public void UpgradeSword(Primary newSwordLevel)
        {
            if (newSwordLevel > SwordLevel)
                SwordLevel = newSwordLevel;
        }

        public void AssignSecondaryItem(Secondary secondaryItem)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (secondaryItem)
            {
                case Secondary.LaserBeam:
                case Secondary.Clock:
                case Secondary.Star:
                case Secondary.Bait:
                    if (ExtraItem1 == Secondary.None)
                    {
                        ExtraItem1 = secondaryItem;
                        SecondaryItem = Secondary.ExtraSlot1;
                    }
                    else if (ExtraItem2 == Secondary.None)
                    {
                        ExtraItem2 = secondaryItem;
                        SecondaryItem = Secondary.ExtraSlot2;
                    }
                    break;
                default:
                    SecondaryItem = secondaryItem;
                    break;
            }
        }

        public void AddSecondaryItem(Secondary secondaryItem)
        {
            switch (secondaryItem)
            {
                case Secondary.Boomerang:
                    HasBoomerang = true;
                    break;
                case Secondary.Bomb:
                    BombCount = Math.Min(BombCount + 4, MaxBombCount);
                    break;
                case Secondary.Bow:
                    if (BowLevel == Secondary.None) BowLevel = Secondary.Bow;
                    break;
                case Secondary.FireBow:
                    BowLevel = Secondary.FireBow;
                    break;
                case Secondary.Arrow:
                    if (ArrowLevel == Secondary.None) ArrowLevel = Secondary.Arrow;
                    break;
                case Secondary.SilverArrow:
                    ArrowLevel = Secondary.SilverArrow;
                    break;
                case Secondary.Coins:
                    Coins = 2;
                    break;
                case Secondary.ATWBoomerang:
                    HasATWBoomerang = true;
                    break;
                case Secondary.BombLauncher:
                    HasBombLauncher = true;
                    break;
                default:
                    break;
            }
        }

        public void AddCoin()        {
            Coins++;
        }

        public void AddMap()
        {
            HasMap = true;
        }

        public void AddCompass()
        {
            HasCompass = true;
        }

        public void Add1Rupee()
        {
            RupeeCount = Math.Min(RupeeCount + RupeeMultiplier, MaxRupeeCount);
        }

        public void Add5Rupee(){
            RupeeCount = Math.Min(RupeeCount + 5 * RupeeMultiplier, MaxRupeeCount);
        }

        public void AddKey()
        {
            KeyCount = Math.Min(KeyCount + 1, MaxKeyCount);
        }

        public bool TryRemoveBoomerang()
        {
            if (!HasBoomerang) return false;
            HasBoomerang = false;
            return true;
        }

        public bool TryRemoveBomb()
        {
            if (BombCount <= 0) return false;
            BombCount--;
            return true;
        }

        public bool TryRemoveCoins()
        {
            if (Coins < 2) return false;
            Coins = 0;
            return true;
        }

        public bool TryRemoveATWBoomerang()
        {
            if (!HasATWBoomerang) return false;
            HasATWBoomerang = false;
            return true;
        }

        public Secondary RemoveExtraItem1()
        {
            var extraItem = ExtraItem1;
            ExtraItem1 = Secondary.None;
            return extraItem;
        }

        public Secondary RemoveExtraItem2()
        {
            var extraItem = ExtraItem2;
            ExtraItem2 = Secondary.None;
            return extraItem;
        }

        public bool TryRemoveRupee(int price = 1)
        {
            if ( ( RupeeCount <= 0 && RupeeCount < price ) || RupeeCount - price < 0) return false;
            RupeeCount = RupeeCount - price;
            return true;
        }

        public bool TryRemoveKey()
        {
            if (KeyCount <= 0) return false;
            KeyCount--;
            return true;
        }

        public void UpgradeRupeeWallet(int newMax)
        {
            MaxRupeeCount = newMax;
        }

        public void UpgradeBombWallet(int newMax)
        {
            MaxBombCount = newMax;
        }

        public void UpgradeRupeeMultiplier(int scale)
        {
            RupeeMultiplier = scale;
        }
    }
}
 