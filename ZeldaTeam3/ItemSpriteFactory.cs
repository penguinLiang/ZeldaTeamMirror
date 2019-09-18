using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
   public class ItemSpriteFactory
    {
        //initial variables for spritesheets
        //IE: private Texture2D _spritesheet;
        private Texture2D _fieldWeaponsSpriteSheet;
        private Texture2D _itemsSpriteSheet;

        /* Field Weapons (64X136) 
         Individual Frame = 16X16
         Boomerang 8X8
         Rows 1-4 = Swords
         Rows 7-8 = fireballs
         9 = boomerang
         
             
             Items (32X192)
                8X16 or 16X16
                Vertical Offset = multiple of 16
                red and blue hearts = 8X8
                */

        private static ItemSpriteFactory _instance = new ItemSpriteFactory();
        public static ItemSpriteFactory Instance => _instance;

        private ItemSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            _fieldWeaponsSpriteSheet = content.Load<Texture2D>("FieldWeapons");
            _itemsSpriteSheet = content.Load<Texture2D>("Items");

        }

        public ISprite CreateDroppedHeart()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateHeartContainer()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateFairy()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateClock()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateBlueRupee()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateRedRupee()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateMap()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateCompass()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateKey()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateWoodBoomerang()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateWoodShield()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateWoodSword()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateBomb()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateBlueRing()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateRedRing()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateBow()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateMagicBoomerang()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateWhiteSword()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateMagicSword()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateArrow()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

        public ISprite CreateTriforcePiece()
        {
            return new Sprite(_itemsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }


        public ISprite CreateFireball()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 32, 192, 2, new Point(0, 0));
        }

    }
}
