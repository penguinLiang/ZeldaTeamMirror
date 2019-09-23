using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public ISprite CurrentSprite { get; set; }
        public IPlayer TemporaryLink { get; set; }
        public ISprite test;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private IController[] _controllers;
        public IEnemy[] Enemies;
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
            _controllers = new IController[]{
                new ControllerKeyboard(this), 
            };

            foreach (IController controller in _controllers)
            {
                _controlsDescription += controller + "\n";
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Arial");
            Texture2D legendOfZeldaSheet = Content.Load<Texture2D>("LegendOfZelda");
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

            CurrentSprite = new Sprite(legendOfZeldaSheet, 34, 54, 4, new Point(0, 94));
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

            foreach (ISprite block in _dungeonBorderBlocks)
            {
                block.Update();
            }

            foreach (ISprite block in _dungeonEnvironmentBlocks)
            {
                block.Update();
            }

            CurrentSprite.Update();
          
            foreach (ISprite item in _items)
            {
                item.Update();
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int xBorderBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yBorderBlocks = 300;
            int xEnvironmentBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yEnvironmentBlocks = 400;
            int x = GraphicsDevice.Viewport.Bounds.Center.X;
            int y = 50;
            _spriteBatch.Begin();
            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());

            foreach (ISprite block in _dungeonBorderBlocks)
            {
                if(xBorderBlocks < (GraphicsDevice.Viewport.Bounds.Right - 100) && yBorderBlocks < GraphicsDevice.Viewport.Bounds.Bottom)
                {
                    xBorderBlocks += 32;
                }
                else
                {
                    xBorderBlocks = (GraphicsDevice.Viewport.Bounds.Center.X - 100) + 32;
                    yBorderBlocks += 32;
                }

                block.Draw(_spriteBatch, new Vector2(xBorderBlocks, yBorderBlocks));
            }

            foreach (ISprite block in _dungeonEnvironmentBlocks)
            {
                if (xEnvironmentBlocks < (GraphicsDevice.Viewport.Bounds.Right - 100) && yEnvironmentBlocks < GraphicsDevice.Viewport.Bounds.Bottom)
                {
                    xEnvironmentBlocks += 32;
                }
                else
                {
                    xEnvironmentBlocks = (GraphicsDevice.Viewport.Bounds.Center.X - 100) + 32;
                    yEnvironmentBlocks += 32;
                }

                block.Draw(_spriteBatch, new Vector2(xEnvironmentBlocks, yEnvironmentBlocks));
            }
            
            foreach(ISprite item in _items)
            {


                if(x < GraphicsDevice.Viewport.Bounds.Right && y < GraphicsDevice.Viewport.Bounds.Bottom)
                {
                    x += 32;
                }
                else
                {
                    x = GraphicsDevice.Viewport.Bounds.Center.X +32;
                    y += 32;
                }

                item.Draw(_spriteBatch, new Vector2(x, y));
            }

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}