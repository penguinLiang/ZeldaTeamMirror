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
    public class BlockSpriteFactory
    {
        private Texture2D _doorSpritesheet;
        private Texture2D _tileSpritesheet;

        private static BlockSpriteFactory _instance = new BlockSpriteFactory();
        public static BlockSpriteFactory Instance => _instance;

        private BlockSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            _doorSpritesheet = content.Load<Texture2D>("Doors");
            _tileSpritesheet = content.Load<Texture2D>("Tiles");
        }

        public ISprite CreateTopWall()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(0,0));
        }

        public ISprite CreateLeftWall()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(0, 32));
        }

        public ISprite CreateRightWall()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(0, 64));
        }

        public ISprite CreateBottomWall()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(0, 96));
        }

        public ISprite CreateTopOpenDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(32, 0));
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(32, 0, 32, 32));
        }

        public ISprite CreateLeftOpenDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(32, 32));
        }

        public ISprite CreateRightOpenDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(32, 64));
        }

        public ISprite CreateBottomOpenDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(32, 96));
        }

        public ISprite CreateTopLockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(64, 0));
        }

        public ISprite CreateLeftLockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(64, 32));
        }

        public ISprite CreateRightLockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(64, 64));
        }

        public ISprite CreateBottomLockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(64, 96));
        }

        public ISprite CreateTopBlockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(96, 0));
        }

        public ISprite CreateLeftBlockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(96, 32));
        }

        public ISprite CreateRightBlockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(96, 64));
        }

        public ISprite CreateBottomBlockedDoor()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(96, 96));
        }

        public ISprite CreateTopWallHole()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(128, 0));
        }

        public ISprite CreateLeftWallHole()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(128, 32));
        }

        public ISprite CreateRightWallHole()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(128, 64));
        }

        public ISprite CreateBottomWallHole()
        {
            return new Sprite(Texture2D _doorSpritesheet, 32, 32, 1, new Point(128, 96));
        }

        public ISprite CreateSolidBlock()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(0, 0));
        }

        public ISprite CreateGapTile()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(16, 0));
        }

        public ISprite CreateStatue1()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(0, 16));
        }

        public ISprite CreateStatue2()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(16, 16));
        }

        public ISprite CreateStairs1()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(0, 32));
        }

        public ISprite CreateStairs2()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(0, 48));
        }

        public ISprite CreateBrickBlock()
        {
            return new Sprite(Texture2D _tileSpritesheet, 16, 16, 1, new Point(16, 48));
        }

        public ISprite CreateFire()
        {
            return new Sprite(Texture2D _tileSpritesheet, 32, 16, 2, new Point(0,60), frameDelay: 15, paletteRowCount: 1, paletteRows: 2);
        }
    }
}
