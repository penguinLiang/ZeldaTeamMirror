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

        private static int _frameCounter = 0;
        private static int _blockListItem = 0;
        private ISprite _displayedBlockSprite;

        private IController[] _controllers;
        public IEnemy[] Enemies;
        private string _controlsDescription = "";

        private ISprite[] _randomBlocksList;

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

            _randomBlocksList = new ISprite[]
            {
                BlockSpriteFactory.Instance.CreateBottomBlockedDoor(),
                BlockSpriteFactory.Instance.CreateBottomLockedDoor(),
                BlockSpriteFactory.Instance.CreateBottomOpenDoor(),
                BlockSpriteFactory.Instance.CreateBottomWall(),
                BlockSpriteFactory.Instance.CreateBottomWallHole(),
                BlockSpriteFactory.Instance.CreateBrickBlock(),
                BlockSpriteFactory.Instance.CreateFire(),
                BlockSpriteFactory.Instance.CreateGapTile(),
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
                BlockSpriteFactory.Instance.CreateSolidBlock(),
                BlockSpriteFactory.Instance.CreateStairs1(),
                BlockSpriteFactory.Instance.CreateStairs2(),
                BlockSpriteFactory.Instance.CreateStatue1(),
                BlockSpriteFactory.Instance.CreateStatue2(),
                BlockSpriteFactory.Instance.CreateTopBlockedDoor(),
                BlockSpriteFactory.Instance.CreateTopLockedDoor(),
                BlockSpriteFactory.Instance.CreateTopOpenDoor(),
                BlockSpriteFactory.Instance.CreateTopWall(),
                BlockSpriteFactory.Instance.CreateTopWallHole()
            };

            _displayedBlockSprite = _randomBlocksList[0];

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

            foreach (ISprite block in _randomBlocksList)
            {
                block.Update();
            }

            CurrentSprite.Update();

            base.Update(gameTime);

            _frameCounter++;

            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if(_frameCounter >= 30)
            {
                _blockListItem++;
                if(_blockListItem >= _randomBlocksList.Length)
                {
                    _blockListItem = 0;
                }
                _displayedBlockSprite = _randomBlocksList[_blockListItem];
                _frameCounter = 0;
            }
            _displayedBlockSprite.Draw(_spriteBatch, new Vector2(500, 200));


            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}