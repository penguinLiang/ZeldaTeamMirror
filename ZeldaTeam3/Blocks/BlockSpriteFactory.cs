using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Blocks
{
    public class BlockSpriteFactory
    {
        private Texture2D _doorSpritesheet;
        private Texture2D _tileSpritesheet;

        public static BlockSpriteFactory Instance { get; } = new BlockSpriteFactory();

        public void LoadAllTextures(ContentManager content)
        {
            _doorSpritesheet = content.Load<Texture2D>("Doors");
            _tileSpritesheet = content.Load<Texture2D>("Tiles");
        }

        public ISprite CreateTopWall()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(0,0));
        }

        public ISprite CreateLeftWall()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(0, 32));
        }

        public ISprite CreateRightWall()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(0, 64));
        }

        public ISprite CreateBottomWall()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(0, 96));
        }

        public ISprite CreateTopOpenDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(32, 0));
        }

        public ISprite CreateLeftOpenDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(32, 32));
        }

        public ISprite CreateRightOpenDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(32, 64));
        }

        public ISprite CreateBottomOpenDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(32, 96));
        }

        public ISprite CreateTopLockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(64, 0));
        }

        public ISprite CreateLeftLockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(64, 32));
        }

        public ISprite CreateRightLockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(64, 64));
        }

        public ISprite CreateBottomLockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(64, 96));
        }

        public ISprite CreateTopBlockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(96, 0));
        }

        public ISprite CreateLeftBlockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(96, 32));
        }

        public ISprite CreateRightBlockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(96, 64));
        }

        public ISprite CreateBottomBlockedDoor()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(96, 96));
        }

        public ISprite CreateTopWallHole()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(128, 0));
        }

        public ISprite CreateLeftWallHole()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(128, 32));
        }

        public ISprite CreateRightWallHole()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(128, 64));
        }

        public ISprite CreateBottomWallHole()
        {
            return new Sprite(_doorSpritesheet, 32, 32, 1, new Point(128, 96));
        }

        public ISprite CreateSolidBlock()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(0, 0));
        }

        public ISprite CreateWater()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(16, 0));
        }

        public ISprite CreateStatue1()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(0, 16));
        }

        public ISprite CreateStatue2()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(16, 16));
        }

        public ISprite CreateStairs1()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(0, 32));
        }

        public ISprite CreateSand()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(16, 32));
        }

        public ISprite CreateStairs2()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(0, 48));
        }

        public ISprite CreateBrickBlock()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(16, 48));
        }

        public ISprite CreateFire()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 2, new Point(0, 64), 15);
        }

        public ISprite CreateBlackTile()
        {
            return new Sprite(_tileSpritesheet, 16, 16, 1, new Point(0, 80));
        }
    }
}
