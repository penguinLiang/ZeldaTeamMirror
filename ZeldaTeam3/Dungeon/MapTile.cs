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

        // Barriers
        InvisibleWall = 8,
        Fire = 9,
        Water = 10,
        Sand = 11,
        FishStatue = 12,
        DragonStatue = 13,
        BasementBricks = 14,
        BlackBarrier = 15,

        // Items
        Heart = 16,
        Key = 17,
        Map = 18,
        Bow = 19,
        Triforce = 20,
        Compass = 21,
        Boomerang = 22,

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