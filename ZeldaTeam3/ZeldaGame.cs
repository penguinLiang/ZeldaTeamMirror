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

        private ISprite _randomBlock;
        private IController[] _controllers;
        public IEnemy[] Enemies;
        private string _controlsDescription = "";

        private ISprite[] _items;
        


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
            _randomBlock = BlockSpriteFactory.Instance.CreateBottomWall();

            CurrentSprite = new Sprite(legendOfZeldaSheet, 34, 54, 4, new Point(0, 94));
            ItemSpriteFactory.Instance.LoadAllTextures(Content);

            _items = new ISprite[]
            {
            ItemSpriteFactory.Instance.CreateArrow(),
            ItemSpriteFactory.Instance.CreateBlueRing(),
            ItemSpriteFactory.Instance.CreateBlueRupee(),
            ItemSpriteFactory.Instance.CreateBomb(),
            ItemSpriteFactory.Instance.CreateBow(),
            ItemSpriteFactory.Instance.CreateClock(),
            ItemSpriteFactory.Instance.CreateCompass(),
            ItemSpriteFactory.Instance.CreateDroppedHeart(),
            ItemSpriteFactory.Instance.CreateFairy(),
            ItemSpriteFactory.Instance.CreateHeartContainer(),
            ItemSpriteFactory.Instance.CreateKey(),
            ItemSpriteFactory.Instance.CreateMagicSword(),
            ItemSpriteFactory.Instance.CreateMap(),
            ItemSpriteFactory.Instance.CreateRedRing(),
            ItemSpriteFactory.Instance.CreateRedRupee(),
            ItemSpriteFactory.Instance.CreateTriforcePiece(),
            ItemSpriteFactory.Instance.CreateWhiteSword(),
            ItemSpriteFactory.Instance.CreateWoodBoomerang(),
            ItemSpriteFactory.Instance.CreateWoodShield(),
            ItemSpriteFactory.Instance.CreateWoodSword()
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

            _randomBlock.Update();

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
            int x = GraphicsDevice.Viewport.Bounds.Center.X;
            int y = 50;
            
            _spriteBatch.Begin();
            _randomBlock.Draw(_spriteBatch, new Vector2(500,200));
            CurrentSprite.Draw(_spriteBatch, GraphicsDevice.Viewport.Bounds.Center.ToVector2());
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