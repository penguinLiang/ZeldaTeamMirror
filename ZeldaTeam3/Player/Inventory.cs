namespace Zelda.Player
{
    internal class Inventory
    {
        private const int MaxRupeeCount = 255;
        private const int MaxBombCount = 8;
        private const int MaxKeyCount = 255;

        public int SwordLevel { get; private set; }
        public bool HasWoodenBoomerang { get; private set; }
        public bool HasSilverBoomerang { get; private set; }
        public int BombCount { get; private set; }
        public bool HasBow { get; private set; }
        public bool HasWoodenArrow { get; private set; }
        public bool HasSilverArrow { get; private set; }
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; private set; }
        public int KeyCount { get; private set; }

        public Inventory()
        {
            SwordLevel = 0;
            HasWoodenBoomerang = false;
            HasSilverBoomerang = true;
            BombCount = MaxBombCount;
            HasBow = false;
            HasWoodenArrow = true;
            HasSilverArrow = true;
            HasMap = false;
            HasCompass = false;
            RupeeCount = MaxRupeeCount;
            KeyCount = MaxKeyCount;
        }

        public void UpgradeSword(int newSwordLevel)
        {
            if (newSwordLevel > SwordLevel)
                SwordLevel = newSwordLevel;
        }

        public void AddWoodenBoomerang()
        {
            HasWoodenBoomerang = true;
        }

        public void AddSilverBoomerang()
        {
            HasSilverBoomerang = true;
        }

        public void AddFourBombs()
        {
            BombCount += 4;
            if (BombCount > MaxBombCount)
                BombCount = MaxBombCount;
        }

        public void RemoveBomb()
        {
            BombCount--;
        }

        public void AddBow()
        {
            HasBow = true;
        }

        public void AddWoodenArrow()
        {
            HasWoodenArrow = true;
        }

        public void AddSilverArrow()
        {
            HasSilverArrow = true;
        }

        public void AddMap()
        {
            HasMap = true;
        }

        public void AddCompass()
        {
            HasCompass = true;
        }

        public void AddRupee()
        {
            RupeeCount++;
            if (RupeeCount > MaxRupeeCount)
                RupeeCount = MaxRupeeCount;
        }

        public void RemoveRupee()
        {
            RupeeCount--;
        }

        public void AddKey()
        {
            KeyCount++;
            if (KeyCount > MaxKeyCount)
                KeyCount = MaxKeyCount;
        }

        public void RemoveKey()
        {
            KeyCount--;
        }
    }
}
 