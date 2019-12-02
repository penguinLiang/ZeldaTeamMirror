using Microsoft.Xna.Framework;
using Zelda.Items;

namespace Zelda.Survival.HUD
{
    public partial class HUDScreen
    {
        private static readonly ISprite Background = HUDSpriteFactory.Instance.CreateBackground();

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
        private static readonly ISprite AlchemyCoin = ItemSpriteFactory.Instance.CreateAlchemyCoin();
        private static readonly ISprite ATWBoomerang = ItemSpriteFactory.Instance.CreateATWBoomerang();
        private static readonly ISprite BombLauncher = ItemSpriteFactory.Instance.CreateBombLauncher();
        private static readonly ISprite LaserBeam = ItemSpriteFactory.Instance.CreateLaserBeam();
        private static readonly ISprite Clock = ItemSpriteFactory.Instance.CreateClock();
        private static readonly ISprite Star = ItemSpriteFactory.Instance.CreateStar();
        private static readonly ISprite Bait = ItemSpriteFactory.Instance.CreateBait();

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