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

        private ISprite _randomBlock;
        private IController[] _controllers;
        public IEnemy[] Enemies;
        private string _controlsDescription = "";

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
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            _randomBlock = BlockSpriteFactory.Instance.CreateBottomWall();
            CurrentSprite = EnemySpriteFactory.Instance.CreateAquamentusFiring();
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

            _randomBlock.Update();

            CurrentSprite.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _randomBlock.Draw(_spriteBatch, new Vector2(500,200));
            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}