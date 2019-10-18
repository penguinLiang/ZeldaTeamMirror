using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Player;
using Zelda.Projectiles;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public IPlayer Link { get; private set; }
        public DungeonManager DungeonManager { get; } = new DungeonManager();
        public JumpMap JumpMap { get; private set; }

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private IUpdatable[] _controllers;
        private int _aliveReset = 160;

        public ZeldaGame()
        {
            // Use 2x size of NES window
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 512, PreferredBackBufferHeight = 448
            };
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprite.SpriteBatch = _spriteBatch;
            _font = Content.Load<SpriteFont>("Arial");

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BackgroundSpriteFactory.Instance.LoadAllTextures(Content);

            JumpMap = new JumpMap(_spriteBatch,Content);

            Link = new Link(new Point(128, 122));

            _controllers = new IUpdatable[]{
                new ControllerKeyboard(this),
                new ControllerMouse(this)
            };

            DungeonManager.LoadDungeonContent(Content, Link);
            DungeonManager.TransitionToRoom(5,2);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var controller in _controllers)
            {
                controller.Update();
            }

            Link.Update();
            if (!Link.Alive && _aliveReset-- == 0)
            {
                DungeonManager.Reset();
                Link.Spawn();
                DungeonManager.TransitionToRoom(5, 2);
                _aliveReset = 160;
            }
            DungeonManager.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f) * Matrix.CreateTranslation(0.0f, 96.0f, 0.0f));

            if (Link.Alive) DungeonManager.Draw();
            Link.Draw();

            _spriteBatch.End();

            _spriteBatch.Begin();

            JumpMap.Draw();
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}