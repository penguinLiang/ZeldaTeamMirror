using System;
using Zelda.Items;

namespace Zelda.Player
{
    public class Inventory
    {
        private const int MaxRupeeCountInitial = 255;
        private const int MaxBombCountInitial = 8;
        private const int MaxKeyCount = 255;

        private int _maxRupeeCount = MaxRupeeCountInitial;
        private int _maxBombCount = MaxRupeeCountInitial;
        private int _rupeeMultiplier = 1;

        public Primary SwordLevel { get; private set; }
        public Secondary SecondaryItem { get; private set; }
        public bool HasBoomerang { get; private set; }
        public int BombCount { get; private set; } = MaxBombCountInitial / 2;
        public Secondary BowLevel { get; private set; }
        public Secondary ArrowLevel { get; private set; }
        public int Coins { get; private set; } = 2;
        public bool HasATWBoomerang { get; private set; }
        public bool HasBombLauncher { get; private set; }
        public Secondary ExtraItem1 { get; private set; } = Secondary.LaserBeam;
        public Secondary ExtraItem2 { get; private set; } = Secondary.Star;
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; private set; } = 0;
        public int KeyCount { get; private set; }

        // For non-invasive backwards compatibility purposes only
        public bool HasArrow => true;
        public bool HasBow => BowLevel != Secondary.None;

        public Inventory(){
        }

        public void UpgradeSword(Primary newSwordLevel)
        {
            if (newSwordLevel > SwordLevel)
                SwordLevel = newSwordLevel;
        }

        public void AssignSecondaryItem(Secondary secondaryItem)
        {
            switch (secondaryItem)
            {
                case Secondary.LaserBeam:
                case Secondary.Clock:
                case Secondary.Star:
                case Secondary.Bait:
                    // Case pre-condition: At least one extra slot is open (should be checked before method call)
                    if (ExtraItem1 == Secondary.None)
                    {
                        ExtraItem1 = secondaryItem;
                        SecondaryItem = Secondary.ExtraSlot1;
                    }
                    else
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
                    BombCount = Math.Min(BombCount + 4, _maxBombCount);
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

        public void AddCoin()
        {
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
            RupeeCount = Math.Min(RupeeCount + _rupeeMultiplier, _maxRupeeCount);
        }

        public void Add5Rupee(){
            RupeeCount = Math.Min(RupeeCount + 5 * _rupeeMultiplier, _maxRupeeCount);
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

        public void UpgradeRupeeWallet(int newMax)
        {
            _maxRupeeCount = newMax;
        }

        public void UpgradeBombWallet(int newMax)
        {
            _maxBombCount = newMax;
        }

        public void UpgradeRupeeMultiplier(int scale)
        {
            _rupeeMultiplier = scale;
        }
    }
}
 