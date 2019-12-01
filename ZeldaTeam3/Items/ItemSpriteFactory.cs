using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Items
{
   public class ItemSpriteFactory
    {
        private Texture2D _itemsSpriteSheet;
        private Texture2D _starSpriteSheet;

        public static ItemSpriteFactory Instance { get; } = new ItemSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _itemsSpriteSheet = content.Load<Texture2D>("Items");
            _starSpriteSheet = content.Load<Texture2D>("Star");
        }

        public ISprite CreateDroppedHeart()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 2, new Point(0, 0));
        }

        public ISprite CreateHeartContainer()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 1, new Point(16, 0));
        }

        public ISprite CreateFairy()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 2, new Point(0, 16));
        }

        public ISprite Create1Rupee()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 2, new Point(16, 16));
        }

        public ISprite Create5Rupee()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(24, 16));
        }
      
        public ISprite CreateMap()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(0, 48));
        }

        public ISprite CreateCompass()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 1, new Point(16, 48));
        }
   
        public ISprite CreateKey()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(16, 64));
        }

        public ISprite CreateClock()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 1, new Point(16, 80));
        }

        public ISprite CreateWoodSword()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(0, 96));
        }

        public ISprite CreateWhiteSword()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(8, 96));
        }

        public ISprite CreateMagicSword()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(16, 96));
        }

        public ISprite CreateBow()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(0, 112));
        }

        public ISprite CreateFireBow()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(24, 96));
        }

        public ISprite CreateWoodBoomerang()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(8, 112));
        }

        public ISprite CreateATWBoomerang()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(16, 112));
        }

        public ISprite CreateBomb()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(24, 112));
        }
      
        public ISprite CreateArrow()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(0, 128));
        }

        public ISprite CreateSilverArrow()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(8, 128));
        }

        public ISprite CreateBombLauncher()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(24, 128));
        }

        public ISprite CreateLaserBeam()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(0, 144));
        }

        public ISprite CreateWalletUpgrade()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(8, 144));
        }

        public ISprite CreateBombWalletUpgrade()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(16, 144));
        }

        public ISprite CreateBait()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(12, 144));
        }

        public ISprite CreateAlchemyCoin()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 1, new Point(0, 160));
        }

        public ISprite CreateRupeeMultiplier()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 1, new Point(16, 160));
        }

        public ISprite CreateTriforcePiece()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 2, new Point(0, 176));
        }

        public ISprite CreateStar()
        {
            return new Sprite(_starSpriteSheet, 16, 16, 4, Point.Zero, 4);
        }
    }
}
