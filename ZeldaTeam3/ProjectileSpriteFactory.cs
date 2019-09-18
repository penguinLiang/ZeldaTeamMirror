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
    public class ProjectileSpriteFactory
    {

        private Texture2D _fieldWeaponsSpriteSheet;
        private Texture2D _bombExplosionSpriteSheet;

        private static ProjectileSpriteFactory _instance = new ProjectileSpriteFactory();
        public static ProjectileSpriteFactory Instance => _instance;

        private ProjectileSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            _fieldWeaponsSpriteSheet = content.Load<Texture2D>("FieldWeapons");
            _bombExplosionSpriteSheet = content.Load<Texture2D>("SpawnExplosion");
         
        }

        public ISprite CreateArrowUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 1, new Point(0, 80));
        }
        public ISprite CreateArrowRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 1, new Point(16, 80));
        }
        public ISprite CreateArrowLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 1, new Point(32, 80));
        }
        public ISprite CreateArrowDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 1, new Point(48, 80));
        }

        public ISprite CreateFireball()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 4, new Point(0, 112));
        }

        public ISprite CreateThrownBoomerang()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 64, 136, 8, new Point(0, 136));
        }

        public ISprite CreateBombExplosion()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 48, 16, 3, new Point(0, 0));
        }

    }
}
