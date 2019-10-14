using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
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

        public Stalfos DebugStalfos { get; private set; }
        public static int DebugCount;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private IUpdatable[] _controllers;
        private string _controlsDescription = "";

        public ZeldaGame()
        {
            // Use 2x size of NES window
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 512, PreferredBackBufferHeight = 488
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

            Link = new Link(new Point(128, 122));
            //TODO: REMOVE
            DebugStalfos = new Stalfos(new Point(158, 122));
            DebugStalfos.Spawn();

            // Controller instanciation expects that IPlayer and IEnemy exist
            _controllers = new IUpdatable[]{
                new ControllerKeyboard(this)
            };

            foreach (var controller in _controllers)
            {
                _controlsDescription += controller + "\n";
            }

            /* SHOULD BE REMOVED! ONLY FOR PROOF */
            var result = Content.Load<int[][]>("Rooms/0-1");
            for (var row = 0; row < result.Length; row++)
            {
                Console.Write($"Row {row,2}: ");
                for (var col = 0; col < result[row].Length; col++)
                {
                    Console.Write($"{result[row][col],3},");
                }
                Console.WriteLine();
            }
            /* END REMOVE */
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var controller in _controllers)
            {
                controller.Update();
            }
            //TODO: REMOVE DEBUG CODE
            if(Link.CollidesWith(DebugStalfos.Bounds))
            {
                DebugCount++;
                Console.WriteLine("INTERSECTION: "+DebugCount);
                Link.EnemyEffect(DebugStalfos).Execute();
            }
            //TODO: REMOVE
            DebugStalfos.Update();
            Link.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f));
            Link.Draw();
            DebugStalfos.Draw();
            _spriteBatch.End();

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(5,5), Color.White);
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}