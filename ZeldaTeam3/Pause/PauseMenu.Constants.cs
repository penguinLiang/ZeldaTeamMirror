using Microsoft.Xna.Framework;
using Zelda.Items;

namespace Zelda.Pause
{
    public partial class PauseMenu
    {
        private const int InventoryGridRows = 2;
        private const int InventoryGridColumns = 4;

        private static readonly Vector2 GridLocation = new Vector2(128, 40);
        private static readonly Vector2 CursorSize = new Vector2(24, 16);
        private static readonly Vector2 SelectedItemLocation = new Vector2(68, 40);

        private static readonly Vector2 MapGridLocation = new Vector2(136, 104);
        private static readonly Vector2 MapGridCoverSize = new Vector2(8, 8);
        private static readonly ISprite PlayerMapDot = PauseSpriteFactory.Instance.CreateLinkIndicator();
        private static readonly ISprite RoomCover = PauseSpriteFactory.Instance.CreateMapCoverSquare();

        private static readonly Point BoomerangPosition = new Point(0, 0);
        private static readonly Point BombPosition = new Point(1, 0);
        private static readonly Point BowPosition = new Point(2, 0);
        private static readonly Point CoinPosition = new Point(3, 0);
        private static readonly Point ATWBoomerangPosition = new Point(0, 1);
        private static readonly Point BombLauncherPosition = new Point(1, 1);
        private static readonly Point Slot7Position = new Point(2, 1);
        private static readonly Point Slot8Position = new Point(3, 1);

        private static readonly Vector2 BoomerangLocation = new Vector2(4, 0);
        private static readonly Vector2 BombLocation = new Vector2(28, 0);
        private static readonly Vector2 ArrowLocation = new Vector2(48, 0);
        private static readonly Vector2 BowLocation = new Vector2(56, 0);
        private static readonly Vector2 CoinLocation = new Vector2(72, 0);
        private static readonly Vector2 ATWBoomerangLocation = new Vector2(4, 16);
        private static readonly Vector2 BombLauncherLocation = new Vector2(28, 16);
        private static readonly Vector2 Slot7Location = new Vector2(52, 16);
        private static readonly Vector2 Slot8Location = new Vector2(76, 16);

        private static readonly ISprite Boomerang = ItemSpriteFactory.Instance.CreateWoodBoomerang();
        private static readonly ISprite Bomb = ItemSpriteFactory.Instance.CreateBomb();
        private static readonly ISprite Arrow = ItemSpriteFactory.Instance.CreateArrow();
        private static readonly ISprite SilverArrow = ItemSpriteFactory.Instance.CreateSilverArrow();
        private static readonly ISprite Bow = ItemSpriteFactory.Instance.CreateBow();
        private static readonly ISprite FireBow = ItemSpriteFactory.Instance.CreateFireBow();
        private static readonly ISprite AlchemyCoin = ItemSpriteFactory.Instance.CreateAlchemyCoin();
        private static readonly ISprite ATWBoomerang = ItemSpriteFactory.Instance.CreateATWBoomerang();
        private static readonly ISprite BombLauncher = ItemSpriteFactory.Instance.CreateBombLauncher();

        private static readonly Vector2 CompassLocation = new Vector2(44, 144);
        private static readonly Vector2 MapLocation = new Vector2(48, 104);
        private static readonly ISprite Compass = ItemSpriteFactory.Instance.CreateCompass();
        private static readonly ISprite Map = ItemSpriteFactory.Instance.CreateMap();

        private static readonly ISprite Background = PauseSpriteFactory.Instance.CreateBackground();
        private static readonly ISprite CursorGrid = PauseSpriteFactory.Instance.CreateCursorFrame();
    }
}