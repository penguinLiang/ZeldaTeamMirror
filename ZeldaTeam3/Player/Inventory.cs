﻿using System;

namespace Zelda.Player
{
    public class Inventory
    {
        private const int MaxRupeeCount = 255;
        private const int MaxBombCount = 8;
        private const int MaxKeyCount = 255;

        public Items.Primary SwordLevel { get; private set; }
        public bool HasBoomerang { get; private set; }
        public int BombCount { get; private set; } = MaxBombCount / 2;
        public bool HasBow { get; private set; }
        public bool HasArrow { get; private set; } = true;
        public bool HasMap { get; private set; }
        public bool HasCompass { get; private set; }
        public int RupeeCount { get; private set; } = MaxRupeeCount / 2;
        public int KeyCount { get; private set; }

        public Inventory()
        {
        }

        public void UpgradeSword(Items.Primary newSwordLevel)
        {
            if (newSwordLevel == Items.Primary.MagicalSword)
            {
                SwordLevel = newSwordLevel;
            }
            else if (newSwordLevel == Items.Primary.WhiteSword && SwordLevel != Items.Primary.MagicalSword)
            {
                SwordLevel = newSwordLevel;
            }
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
                    break;
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

        public void AddRupee()
        {
            RupeeCount = Math.Min(RupeeCount + 1, MaxRupeeCount);
        }

        public void AddKey()
        {
            KeyCount = Math.Min(KeyCount + 1, MaxKeyCount);
        }

        public void RemoveBomb()
        {
            BombCount--;
        }

        public void RemoveRupee()
        {
            RupeeCount--;
        }

        public void RemoveKey()
        {
            KeyCount--;
        }
    }
}
 