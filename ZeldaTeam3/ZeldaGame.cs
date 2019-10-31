using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.GameState;
using Zelda.HUD;
using Zelda.Items;
using Zelda.JumpMap;
using Zelda.Music;
using Zelda.Pause;
using Zelda.Player;
using Zelda.Projectiles;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public GameStateAgent GameStateAgent { get; private set; }
        public IPlayer Link => GameStateAgent.Player;
        public DungeonManager DungeonManager  => GameStateAgent.DungeonManager;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _aliveReset = 460;

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
            DrawnText.SpriteBatch = _spriteBatch;
            DrawnText.SpriteFont = Content.Load<SpriteFont>("prstartk");

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BackgroundSpriteFactory.Instance.LoadAllTextures(Content);
            PauseSpriteFactory.Instance.LoadAllTextures(Content);
            HUDSpriteFactory.Instance.LoadAllTextures(Content);
            JumpMapScreen.LoadTexture(Content);
            MusicManager.Instance.LoadAllSounds(Content);

            GameStateAgent = new GameStateAgent(_spriteBatch);
            GameStateAgent.DungeonManager.LoadDungeonContent(Content);
            GameStateAgent.Reset();

            // TODO: REMOVE start {
            Console.WriteLine("Enabled Rooms:");
            for (var row = 0; row < DungeonManager.EnabledRooms.Length; row++)
            {
                for (var col = 0; col < DungeonManager.EnabledRooms[row].Length; col++)
                {
                    Console.Write("{0,5}", DungeonManager.EnabledRooms[row][col]);
                    Console.Write(", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Current Room: {0}", DungeonManager.CurrentRoom);
            // TODO: REMOVE end }

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameStateAgent.Quitting)
            {
                Exit();
                return;
            }

            base.Update(gameTime);

            GameStateAgent.Update();

            if (Link.Alive || _aliveReset-- != 0) return;
            GameStateAgent.Continue();
            _aliveReset = 460;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameStateAgent.Draw();
            base.Draw(gameTime);
        }
    }
}