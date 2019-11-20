using System;
using Zelda.Items;

namespace Zelda.Player
{
    public class Inventory
    {
        private const int MaxRupeeCount = 255;
        private const int MaxBombCount = 8;
        private const int MaxKeyCount = 255;

        public Primary SwordLevel { get; private set; }
        public Secondary SecondaryItem { get; private set; }
        public bool HasBoomerang { get; private set; }
        public int BombCount { get; private set; } = MaxBombCount / 2;
        public Secondary BowLevel { get; private set; } = Secondary.FireBow;
        public Secondary ArrowLevel { get; private set; } = Secondary.Arrow;
        public bool HasCoins { get; private set; } = true;
        public bool HasATWBoomerang { get; private set; } = true;
        public bool HasBombLauncher { get; private set; } = true;
        public Secondary ExtraItem1 { get; private set; } = Secondary.None;
        public Secondary ExtraItem2 { get; private set; } = Secondary.None;
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; private set; } = MaxRupeeCount / 2;
        public int KeyCount { get; private set; }

        public void UpgradeSword(Primary newSwordLevel)
        {
            if (newSwordLevel > SwordLevel)
                SwordLevel = newSwordLevel;
        }

        public void AssignSecondaryItem(Secondary secondaryItem)
        {
            SecondaryItem = secondaryItem;
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
                    HasCoins = true;
                    break;
                case Secondary.ATWBoomerang:
                    HasATWBoomerang = true;
                    break;
                case Secondary.BombLauncher:
                    HasBombLauncher = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool TryAddSingleUseItem(Secondary secondaryItem) // Method might be unnecessary if checks are done at shop purchase
        {
            if (ExtraItem1 != Secondary.None && ExtraItem2 != Secondary.None) return false;
            if (ExtraItem1 == Secondary.None)
                ExtraItem1 = secondaryItem;
            else
                ExtraItem2 = secondaryItem;
            return true;
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
            RupeeCount = Math.Min(RupeeCount + 1, MaxRupeeCount);
        }

        public void Add5Rupee(){
            RupeeCount = Math.Min(RupeeCount + 5, MaxRupeeCount);
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
            if (!HasCoins) return false;
            HasCoins = false;
            return true;
        }

        public bool TryRemoveATWBoomerang()
        {
            if (!HasATWBoomerang) return false;
            HasATWBoomerang = false;
            return true;
        }

        public bool TryRemoveExtraItem1()
        {
            if (ExtraItem1 == Secondary.None) return false;
            ExtraItem1 = Secondary.None;
            return true;
        }

        public bool TryRemoveExtraItem2()
        {
            if (ExtraItem2 == Secondary.None) return false;
            ExtraItem2 = Secondary.None;
            return true;
        }

        public bool TryRemoveRupee()
        {
            if (RupeeCount <= 0) return false;
            RupeeCount--;
            return true;
        }

        public bool TryRemoveKey()
        {
            if (KeyCount <= 0) return false;
            KeyCount--;
            return true;
        }
    }
}
 