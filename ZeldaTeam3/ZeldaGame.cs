using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public ISprite CurrentSprite { get; set; }
        public IPlayer TemporaryLink { get; set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private IController[] _controllers;
        public IEnemy[] Enemies;
        private string _controlsDescription = "";

        private ISprite[] _dungeonBorderBlocks;
        private ISprite[] _dungeonEnvironmentBlocks;

        public ZeldaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            int xBorderBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yBorderBlocks = 300;
            int xEnvironmentBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            int yEnvironmentBlocks = 400;

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

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}