using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enemies;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public IPlayer TemporaryLink { get; set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private IController[] _controllers;
        public IEnemy[] Enemies { get; set; }
        private string _controlsDescription = "";

        private ISprite[] _items;
        private ISprite[] _dungeonBorderBlocks;
        private ISprite[] _dungeonEnvironmentBlocks;

        public ZeldaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Arial");

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            Enemies = new IEnemy[]
            {
                new Gel(_spriteBatch, 500, 600),
                new Goriya(_spriteBatch, 550, 600), 
                new Keese(_spriteBatch, 600, 600),
                new Stalfos(_spriteBatch, 650, 600),
                new Trap(_spriteBatch, 700, 600), 
                new WallMaster(_spriteBatch, 750, 600),
                new Aquamentus(_spriteBatch, 800, 600),
                new OldMan(_spriteBatch, 450, 600)
            };

            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            _dungeonBorderBlocks = new ISprite[]
            {
                BlockSpriteFactory.Instance.CreateBottomBlockedDoor(),
                BlockSpriteFactory.Instance.CreateBottomLockedDoor(),
                BlockSpriteFactory.Instance.CreateBottomOpenDoor(),
                BlockSpriteFactory.Instance.CreateBottomWall(),
                BlockSpriteFactory.Instance.CreateBottomWallHole(),
                BlockSpriteFactory.Instance.CreateLeftBlockedDoor(),
                BlockSpriteFactory.Instance.CreateLeftLockedDoor(),
                BlockSpriteFactory.Instance.CreateLeftOpenDoor(),
                BlockSpriteFactory.Instance.CreateLeftWall(),
                BlockSpriteFactory.Instance.CreateLeftWallHole(),
                BlockSpriteFactory.Instance.CreateRightBlockedDoor(),
                BlockSpriteFactory.Instance.CreateRightLockedDoor(),
                BlockSpriteFactory.Instance.CreateRightOpenDoor(),
                BlockSpriteFactory.Instance.CreateRightWall(),
                BlockSpriteFactory.Instance.CreateRightWallHole(),
                BlockSpriteFactory.Instance.CreateTopBlockedDoor(),
                BlockSpriteFactory.Instance.CreateTopLockedDoor(),
                BlockSpriteFactory.Instance.CreateTopOpenDoor(),
                BlockSpriteFactory.Instance.CreateTopWall(),
                BlockSpriteFactory.Instance.CreateTopWallHole()
            };

            _dungeonEnvironmentBlocks = new ISprite[]
            {
                BlockSpriteFactory.Instance.CreateBrickBlock(),
                BlockSpriteFactory.Instance.CreateFire(),
                BlockSpriteFactory.Instance.CreateGapTile(),
                BlockSpriteFactory.Instance.CreateSolidBlock(),
                BlockSpriteFactory.Instance.CreateStairs1(),
                BlockSpriteFactory.Instance.CreateStairs2(),
                BlockSpriteFactory.Instance.CreateStatue1(),
                BlockSpriteFactory.Instance.CreateStatue2()
            };



            Items.ItemSpriteFactory.Instance.LoadAllTextures(Content);

            _items = new ISprite[]
            {
            Items.ItemSpriteFactory.Instance.CreateArrow(),
            Items.ItemSpriteFactory.Instance.CreateBlueRing(),
            Items.ItemSpriteFactory.Instance.CreateBlueRupee(),
            Items.ItemSpriteFactory.Instance.CreateBomb(),
            Items.ItemSpriteFactory.Instance.CreateBow(),
            Items.ItemSpriteFactory.Instance.CreateClock(),
            Items.ItemSpriteFactory.Instance.CreateCompass(),
            Items.ItemSpriteFactory.Instance.CreateDroppedHeart(),
            Items.ItemSpriteFactory.Instance.CreateFairy(),
            Items.ItemSpriteFactory.Instance.CreateHeartContainer(),
            Items.ItemSpriteFactory.Instance.CreateKey(),
            Items.ItemSpriteFactory.Instance.CreateMagicSword(),
            Items.ItemSpriteFactory.Instance.CreateMap(),
            Items.ItemSpriteFactory.Instance.CreateRedRing(),
            Items.ItemSpriteFactory.Instance.CreateRedRupee(),
            Items.ItemSpriteFactory.Instance.CreateTriforcePiece(),
            Items.ItemSpriteFactory.Instance.CreateWhiteSword(),
            Items.ItemSpriteFactory.Instance.CreateWoodBoomerang(),
            Items.ItemSpriteFactory.Instance.CreateWoodShield(),
            Items.ItemSpriteFactory.Instance.CreateWoodSword()
            };

            _controllers = new IController[]{
                new ControllerKeyboard(this)
            };

            foreach (IController controller in _controllers)
            {
                _controlsDescription += controller + "\n";
            }
        }

        protected override void UnloadContent()
        {


        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in _controllers)
            {
                controller.Update();
            }

            foreach (IEnemy enemy in Enemies)
            {
                enemy.Update();
            }

            foreach (ISprite block in _dungeonBorderBlocks)
            {
                block.Update();
            }

            foreach (ISprite block in _dungeonEnvironmentBlocks)
            {
                block.Update();
            }

          
            foreach (ISprite item in _items)
            {
                item.Update();
            }

            base.Update(gameTime);

        }

        private void DrawSpriteGrid(ISprite[] spriteArray, int x, int y)
        {
            foreach (ISprite sprite in spriteArray)
            {
                if (x < GraphicsDevice.Viewport.Bounds.Right && y < GraphicsDevice.Viewport.Bounds.Bottom)
                {
                    x += 32;
                }
                else
                {
                    x = GraphicsDevice.Viewport.Bounds.Center.X + 32;
                    y += 32;
                }

                sprite.Draw(_spriteBatch, new Vector2(x, y));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int xBorderBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yBorderBlocks = 300;
            int xEnvironmentBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yEnvironmentBlocks = 400;
            int xItems = GraphicsDevice.Viewport.Bounds.Center.X;
            int yItems = 50;
            
            _spriteBatch.Begin();

            DrawSpriteGrid(_dungeonBorderBlocks, xBorderBlocks, yBorderBlocks);
            DrawSpriteGrid(_dungeonEnvironmentBlocks, xEnvironmentBlocks, yEnvironmentBlocks);
            DrawSpriteGrid(_items, xItems, yItems);

            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw();
            }

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}