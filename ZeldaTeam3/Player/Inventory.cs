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
        public bool HasBow { get; private set; }
        public bool HasArrow { get; private set; } = true;
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
                case Secondary.Bow:
                    HasBow = true;
                    break;
                case Secondary.Bomb:
                    BombCount = Math.Min(BombCount + 4, MaxBombCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddArrow()
        {
            HasArrow = true;
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

        public bool TryRemoveBomb()
        {
            if (BombCount <= 0) return false;
            BombCount--;
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

        public bool TryRemoveBoomerang()
        {
            if (!HasBoomerang) return false;
            HasBoomerang = false;
            return true;
        }
    }
}
 