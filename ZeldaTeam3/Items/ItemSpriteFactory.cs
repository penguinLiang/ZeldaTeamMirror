using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Items
{
   public class ItemSpriteFactory
    {
        private Texture2D _itemsSpriteSheet;

        public static ItemSpriteFactory Instance { get; } = new ItemSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _itemsSpriteSheet = content.Load<Texture2D>("Items");
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

        public ISprite CreateWoodBoomerang()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(8, 112));
        }
     
        public ISprite CreateBomb()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16, 1, new Point(24, 112));
        }
      
        public ISprite CreateArrow()
        {
            return new Sprite(_itemsSpriteSheet, 8, 16,1, new Point(0, 128));
        }

        public ISprite CreateTriforcePiece()
        {
            return new Sprite(_itemsSpriteSheet, 16, 16, 2, new Point(0, 176));
        }

    }
}
