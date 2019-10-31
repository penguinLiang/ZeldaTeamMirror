using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Music;
using Zelda.Player;
using Zelda.Projectiles;
using Zelda.Pause;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public bool Resetting { get; set; }
        public IPlayer Link { get; private set; }
        public DungeonManager DungeonManager { get; } = new DungeonManager();
        public JumpMap JumpMap { get; private set; }
        public PauseMenu PauseMenu { get; private set; }
        public MusicManager music; 

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BackgroundSpriteFactory.Instance.LoadAllTextures(Content);
            PauseSpriteFactory.Instance.LoadAllTextures(Content);

            MusicManager.Instance.LoadAllSounds(Content);

            JumpMap = new JumpMap(_spriteBatch,Content);

            Link = new Link(new Point(128, 122));

            DungeonManager.LoadDungeonContent(Content, Link);
            DungeonManager.TransitionToRoom(5,2);

            PauseMenu = new PauseMenu(_spriteBatch,Content,Link,DungeonManager);

            _controllers = new IUpdatable[]{
                new ControllerKeyboard(this),
                new ControllerMouse(this),
                new ControllerPauseKeyboard(this)
            };

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
            PauseMenu.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f) * Matrix.CreateTranslation(0.0f, 96.0f, 0.0f));

            if (Link.Alive) DungeonManager.Draw();
            Link.Draw();

            PauseMenu.Draw();

            _spriteBatch.End();

            _spriteBatch.Begin();

            JumpMap.Draw();
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}