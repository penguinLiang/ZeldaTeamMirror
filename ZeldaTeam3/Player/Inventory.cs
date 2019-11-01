using System;

namespace Zelda.Player
{
    public class Inventory
    {
        private const int MaxRupeeCount = 255;
        private const int MaxBombCount = 8;
        private const int MaxKeyCount = 255;

        public Items.Primary SwordLevel { get; private set; }
        public Items.Secondary SecondaryItem { get; private set; }
        public bool HasBoomerang { get; private set; }
        public int BombCount { get; private set; } = MaxBombCount / 2;
        public bool HasBow { get; private set; }
        public bool HasArrow { get; private set; } = true;
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; private set; } = MaxRupeeCount / 2;
        public int KeyCount { get; private set; }

        public void UpgradeSword(Items.Primary newSwordLevel)
        {
            if (newSwordLevel > SwordLevel)
                SwordLevel = newSwordLevel;
        }

        public void AssignSecondaryItem(Items.Secondary secondaryItem)
        {
            SecondaryItem = secondaryItem;
        }

        public void AddSecondaryItem(Items.Secondary secondaryItem)
        {
            switch (secondaryItem)
            {
                case Items.Secondary.Boomerang:
                    HasBoomerang = true;
                    break;
                case Items.Secondary.Bow:
                    HasBow = true;
                    break;
                case Items.Secondary.Bomb:
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
    }
}
 