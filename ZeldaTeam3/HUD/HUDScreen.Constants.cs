using Microsoft.Xna.Framework;
using Zelda.Items;

namespace Zelda.HUD
{
    public partial class HUDScreen
    {
        private static readonly ISprite Background = HUDSpriteFactory.Instance.CreateBackground();

        private static readonly Vector2 MiniMapLocation = new Vector2(24, 17);
        private static readonly Vector2 MiniMapCellSize = new Vector2(8, 4);
        private static readonly Vector2 TriforceLocation = new Vector2(40, 4);
        private static readonly ISprite MiniMap = HUDSpriteFactory.Instance.CreateDungeonLayout();
        private static readonly ISprite PlayerDot = HUDSpriteFactory.Instance.CreateLinkIndicator();
        private static readonly ISprite TriforceDot = HUDSpriteFactory.Instance.CreateTriforceIndicator();

        private static readonly Vector2 PrimaryLocation = new Vector2(152, 16);
        private static readonly ISprite WoodSword = ItemSpriteFactory.Instance.CreateWoodSword();
        private static readonly ISprite WhiteSword = ItemSpriteFactory.Instance.CreateWhiteSword();
        private static readonly ISprite MagicSword = ItemSpriteFactory.Instance.CreateMagicSword();

        private static readonly Vector2 SecondaryLocation8_16 = new Vector2(128, 16);
        private static readonly Vector2 SecondaryLocation16_16 = new Vector2(124, 16);
        private static readonly ISprite Bomb = ItemSpriteFactory.Instance.CreateBomb();
        private static readonly ISprite Boomerang = ItemSpriteFactory.Instance.CreateWoodBoomerang();
        private static readonly ISprite Arrow = ItemSpriteFactory.Instance.CreateArrow();
        private static readonly ISprite SilverArrow = ItemSpriteFactory.Instance.CreateSilverArrow();
        private static readonly ISprite Bow = ItemSpriteFactory.Instance.CreateBow();
        private static readonly ISprite FireBow = ItemSpriteFactory.Instance.CreateFireBow();
        private static readonly ISprite AlchemyCoin = ItemSpriteFactory.Instance.CreateAlchemyCoin();
        private static readonly ISprite ATWBoomerang = ItemSpriteFactory.Instance.CreateATWBoomerang();
        private static readonly ISprite BombLauncher = ItemSpriteFactory.Instance.CreateBombLauncher();

        private static readonly Vector2 HeartsLocation = new Vector2(176, 32);
        private static readonly Vector2 HeartOffset = new Vector2(8, 0);
        private static readonly ISprite Heart = HUDSpriteFactory.Instance.CreateFullHeart();
        private static readonly ISprite HalfHeart = HUDSpriteFactory.Instance.CreateHalfHeart();
        private static readonly ISprite EmptyHeart = HUDSpriteFactory.Instance.CreateEmptyHeart();

        private static readonly Point RuppeeCountLocation = new Point(96, 8);
        private static readonly Point KeyCountLocation = new Point(96, 24);
        private static readonly Point BombCountLocation = new Point(96, 32);
    }
}