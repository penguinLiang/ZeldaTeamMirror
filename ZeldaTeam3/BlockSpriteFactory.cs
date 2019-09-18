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
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(0, 0, 32, 32));
        }

        public ISprite CreateLeftWall()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(0, 32, 32, 32));
        }

        public ISprite CreateRightWall()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(0, 64, 32, 32));
        }

        public ISprite CreateBottomWall()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(0, 96, 32, 32));
        }

        public ISprite CreateTopOpenDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(32, 0, 32, 32));
        }

        public ISprite CreateLeftOpenDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(32, 32, 32, 32));
        }

        public ISprite CreateRightOpenDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(32, 64, 32, 32));
        }

        public ISprite CreateBottomOpenDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(32, 96, 32, 32));
        }

        public ISprite CreateTopLockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(64, 0, 32, 32));
        }

        public ISprite CreateLeftLockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(64, 32, 32, 32));
        }

        public ISprite CreateRightLockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(64, 64, 32, 32));
        }

        public ISprite CreateBottomLockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(64, 96, 32, 32));
        }

        public ISprite CreateTopBlockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(96, 0, 32, 32));
        }

        public ISprite CreateLeftBlockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(96, 32, 32, 32));
        }

        public ISprite CreateRightBlockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(96, 64, 32, 32));
        }

        public ISprite CreateBottomBlockedDoor()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(96, 96, 32, 32));
        }

        public ISprite CreateTopWallHole()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(128, 0, 32, 32));
        }

        public ISprite CreateLeftWallHole()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(128, 32, 32, 32));
        }

        public ISprite CreateRightWallHole()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(128, 64, 32, 32));
        }

        public ISprite CreateBottomWallHole()
        {
            return new SpriteFixedStatic(_doorSpritesheet, new Rectangle(128, 96, 32, 32));
        }

        public ISprite CreateSolidBlock()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(0, 0, 16, 16));
        }

        public ISprite CreateGapTile()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(16, 0, 16, 16));
        }

        public ISprite CreateStatue1()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(0, 16, 16, 16));
        }

        public ISprite CreateStatue2()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(16, 16, 16, 16));
        }

        public ISprite CreateStairs1()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(0, 32, 16, 16));
        }

        public ISprite CreateStairs2()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(0, 48, 16, 16));
        }

        public ISprite CreateBrickBlock()
        {
            return new SpriteFixedStatic(_tileSpritesheet, new Rectangle(16, 48, 16, 16));
        }

        public ISprite CreateFire()
        {
            Rectangle _fireFrameOne = new Rectangle(0, 60, 16, 16);
            Rectangle _fireFrameTwo = new Rectangle(16, 60, 16, 16);
            Rectangle[] _frameBounceArray = [_fireFrameOne, _fireFrameTwo];
            return new SpriteFixedAnimated(_tileSpritesheet, _frameBounceArray);
        }
    }
}
