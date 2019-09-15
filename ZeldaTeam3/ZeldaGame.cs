using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public ISprite CurrentSprite { get; set; }

        // This is temporary Link variable for implementing stuff
        public IPlayer TemporaryLink { get; set; }

        // This is temporary enemy variable for implementing stuff
        public IEnemy TemporaryEnemy { get; set; }

        public ISprite StandingLink { get; private set; }
        public ISprite StabbingLink { get; private set; }
        public ISprite JumpingLink { get; private set; }
        public ISprite WalkingLink { get; private set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private IController[] _controllers;
        private string _controlsDescription = "";

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

            StandingLink = new SpriteFixedStatic(legendOfZeldaSheet, new Rectangle(426, 22, 32, 32));
            StabbingLink = new SpriteFixedAnimated(legendOfZeldaSheet, new []
            {
                new Rectangle(2, 154, 32, 32),
                new Rectangle(36, 154, 54, 32),
                new Rectangle(92, 154, 46, 32),
                new Rectangle(140, 154, 38, 32) 
            });
            JumpingLink = new SpriteMovingStatic(legendOfZeldaSheet, new Rectangle(460, 22, 32, 32), 64);
            WalkingLink = new SpriteMovingAnimated(legendOfZeldaSheet, new []
            {
                new Rectangle(70, 22, 32, 32), 
                new Rectangle(104, 22, 32, 32), 
            });

            CurrentSprite = StandingLink;
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

            CurrentSprite.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}