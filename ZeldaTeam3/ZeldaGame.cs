using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enemies;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public ISprite CurrentSprite { get; set; }
        public IPlayer TemporaryLink { get; set; }

        public IEnemy TemporaryEnemy { get; set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private ISprite _randomBlock;
        private IController[] _controllers;
        public IEnemy[] Enemies { get; set; }
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
            TemporaryEnemy = new Stalfos(_spriteBatch, 600, 400);
            Gel gel = new Gel(_spriteBatch, 632, 400);
            Keese keese = new Keese(_spriteBatch, 664, 400);
            WallMaster wallMaster = new WallMaster(_spriteBatch, 696, 400);
            Trap trap = new Trap(_spriteBatch, 732, 400);
            Goriya goriya = new Goriya(_spriteBatch, 764, 400);
            

            Enemies = new IEnemy[] { TemporaryEnemy, gel, keese, wallMaster, trap, goriya};

            foreach (IEnemy enemy in Enemies)
            {
                enemy.Spawn();
            }

            _controllers = new IController[]{
                new ControllerKeyboard(this),
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

            _randomBlock.Update();
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Update();
            }

            CurrentSprite.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _randomBlock.Draw(_spriteBatch, new Vector2(500,200));
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw();
            }
            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}