using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Enemies
{
    public class EnemySpriteFactory
    {
        public static EnemySpriteFactory Instance { get; } = new EnemySpriteFactory();

        private Texture2D _stalfosTexture2D;
        private Texture2D _keeseGelTrapTexture2D;
        private Texture2D _wallMasterTexture2D;
        private Texture2D _goriyaTexture2D;
        private Texture2D _aquamentusTexture2D;
        private Texture2D _oldManTexture2D;

        public void LoadAllTextures(ContentManager content)
        {
            _stalfosTexture2D = content.Load<Texture2D>("EnemySkeleton");
            _keeseGelTrapTexture2D = content.Load<Texture2D>("EnemyOther");
            _wallMasterTexture2D = content.Load<Texture2D>("EnemyHand");
            _goriyaTexture2D = content.Load<Texture2D>("EnemyGoriya");
            _aquamentusTexture2D = content.Load<Texture2D>("Boss");
            _oldManTexture2D = content.Load<Texture2D>("OldMan");
        }

        public ISprite CreateStalfos()
        {
            return new Sprite(_stalfosTexture2D, 16, 16, 2, new Point(0,0), 20, 16, 2);
        }

        public ISprite CreateKeese()
        {
            return new Sprite(_keeseGelTrapTexture2D, 16, 16, 2, new Point(0,0));
        }

        public ISprite CreateOldMan()
        {
            return new Sprite(_oldManTexture2D, 16, 16, 1, new Point(0, 0), 20, 16, 2);
        }

        public ISprite CreateTrap()
        {
            return new Sprite(_keeseGelTrapTexture2D, 16, 16, 1, new Point(0, 32));
        }

        public ISprite CreateWallMaster()
        {
            return new Sprite(_wallMasterTexture2D, 16, 16, 2, new Point(0, 0), 20, 16, 2);
        }

        public ISprite CreateGel()
        {
            return new Sprite(_keeseGelTrapTexture2D, 16, 16, 2, new Point(0, 16), 5);
        }

        public ISprite CreateAquamentusIdle()
        {
            return new Sprite(_aquamentusTexture2D, 24, 32, 2, new Point(0, 0), 20, 64, 2);
        }

        public ISprite CreateAquamentusFiring()
        {
            return new Sprite(_aquamentusTexture2D, 24, 32, 2, new Point(0, 32));
        }

        public ISprite CreateGoriyaFaceDown()
        {
            return new Sprite(_goriyaTexture2D, 16, 16, 2, new Point(0, 0), 20, 64, 2);
        }

        public ISprite CreateGoriyaFaceUp()
        {
            return new Sprite(_goriyaTexture2D, 16, 16, 2, new Point(0, 16),20, 64, 2);
        }

        public ISprite CreateGoriyaFaceLeft()
        {
            return new Sprite(_goriyaTexture2D, 16, 16, 2, new Point(0, 48), 20, 64, 2);
        }

        public ISprite CreateGoriyaFaceRight()
        {
            return new Sprite(_goriyaTexture2D, 16, 16, 2, new Point(0, 32), 20, 64, 2);
        }
    }
}
