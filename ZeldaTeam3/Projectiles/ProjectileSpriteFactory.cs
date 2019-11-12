using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Projectiles
{
    public class ProjectileSpriteFactory
    {
        private Texture2D _fieldWeaponsSpriteSheet;
        private Texture2D _bombExplosionSpriteSheet;
        private Texture2D _particlesSpriteSheet;

        public static ProjectileSpriteFactory Instance { get; } = new ProjectileSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _fieldWeaponsSpriteSheet = content.Load<Texture2D>("FieldWeapons");
            _bombExplosionSpriteSheet = content.Load<Texture2D>("SpawnExplosion");
            _particlesSpriteSheet = content.Load<Texture2D>("Particles");
        }

        public ISprite CreateArrowUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 64));
        }
        public ISprite CreateArrowRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(16, 64));
        }
        public ISprite CreateArrowLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(32, 64));
        }
        public ISprite CreateArrowDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(48, 64));
        }

        public ISprite CreateAquamentusFireball()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 96), 4);
        }

        public ISprite CreateOldManFireball()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 112), 4);
        }

        public ISprite CreateThrownBoomerang()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 8, 8, 8, new Point(0, 128), 5);
        }

        public ISprite CreateBomb()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 1, new Point(0, 80));
        }

        public ISprite CreateBombExplosion()
        {
            return new Sprite(_bombExplosionSpriteSheet, 16, 16, 3, new Point(0, 0));
        }

        public ISprite CreateSwordBeamUp()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 0), 4);
        }

        public ISprite CreateSwordBeamRight()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 16), 4);
        }

        public ISprite CreateSwordBeamLeft()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 32), 4);
        }

        public ISprite CreateSwordBeamDown()
        {
            return new Sprite(_fieldWeaponsSpriteSheet, 16, 16, 4, new Point(0, 48), 4);
        }

        public ISprite CreateSwordBeamParticleTopLeft()
        {
            return new Sprite(_particlesSpriteSheet, 16, 16, 4, new Point(0, 0), 4);
        }

        public ISprite CreateSwordBeamParticleTopRight()
        {
            return new Sprite(_particlesSpriteSheet, 16, 16, 4, new Point(0, 16), 4);
        }

        public ISprite CreateSwordBeamParticleBottomLeft()
        {
            return new Sprite(_particlesSpriteSheet, 16, 16, 4, new Point(0, 32), 4);
        }

        public ISprite CreateSwordBeamParticleBottomRight()
        {
            return new Sprite(_particlesSpriteSheet, 16, 16, 4, new Point(0, 48), 4);
        }

        public ISprite CreateBoomerangCollision()
        {
            return new Sprite(_particlesSpriteSheet, 8, 8, 1, new Point(0, 64));
        }
    }
}