namespace Zelda.Dungeon
{
    public enum MapTile
    {
        None = -1,

        // Misc
        SpawnEnemy = 0,
        BlackOverlay = 1,
        DungeonStairs = 2,
        BasementStairs = 3,
        SpawnShopKeep = 4,

        // Barriers
        ProjectileBlackBarrier = 5,
        InvisibleWall = 8,
        Fire = 9,
        Water = 10,
        Sand = 11,
        FishStatue = 12,
        DragonStatue = 13,
        BasementBricks = 14,
        BlackBarrier = 15,
        RupeeBarrier = 27,
        KeyBarrier = 28,


        // Items
        Heart = 16,
        Key = 17,
        Map = 18,
        Bow = 19,
        Triforce = 20,
        Compass = 21,
        Boomerang = 22,
        SilverArrow = 23,
        FireBow = 29, 
        WhiteSword = 30,
        MagicSword = 31,
        AlchemyCoin = 38,
        ATWBoomerang = 39,
        WalletUpgrade = 44,
        Bomb = 45,
        BombUpgrade = 46, 
        Bait = 47,
        Clock = 52,
        Star = 53,
        CrossShot = 59,
        Arrow = 60,
        BombLauncher = 61,
        RupeeUpgrade = 54,
        Fairy = 36,

        // Blocks
        ImmovableBlock = 24,
        PushableBlock = 25,
        Room2_1Block = 26,

        // Doors
        DoorUp = 32,
        DoorDown = 33,
        DoorRight = 34,
        DoorLeft = 35,

        // Locked Doors
        DoorLockedUp = 40,
        DoorLockedLeft = 41,
        DoorLockedDown = 42,
        DoorLockedRight = 43,

        // Bombable Doors
        DoorBombableUp = 48,
        DoorBombableLeft = 49,
        DoorBombableDown = 50,
        DoorBombableRight = 51,

        // Special Doors
        DoorSpecialLeft2_1 = 56,
        DoorSpecialRight3_1 = 57,
        StairSpecialUp1_1 = 58
    }
}