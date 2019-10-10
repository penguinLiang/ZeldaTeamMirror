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

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private IController[] _controllers;
        public IEnemy[] Enemies { get; set; }
        private string _controlsDescription = "";

        private ISprite[] _items;
        private ISprite[] _dungeonBorderBlocks;
        private ISprite[] _dungeonEnvironmentBlocks;

        public ZeldaGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1024, PreferredBackBufferHeight = 768
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

            Enemies = new IEnemy[]
            {
                new Gel(250, 300),
                new Goriya(275, 300), 
                new Keese(600/2, 300),
                new Stalfos(325, 300),
                new Trap(350, 300), 
                new WallMaster(375, 300),
                new Aquamentus(400, 300),
                new OldMan(225, 300)
            };

            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            _dungeonBorderBlocks = new[]
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

            _dungeonEnvironmentBlocks = new[]
            {
                BlockSpriteFactory.Instance.CreateBrickBlock(),
                BlockSpriteFactory.Instance.CreateFire(),
                BlockSpriteFactory.Instance.CreateSolidBlock(),
                BlockSpriteFactory.Instance.CreateStairs1(),
                BlockSpriteFactory.Instance.CreateStairs2(),
                BlockSpriteFactory.Instance.CreateStatue1(),
                BlockSpriteFactory.Instance.CreateStatue2()
            };

            ItemSpriteFactory.Instance.LoadAllTextures(Content);

            _items = new[]
            {
                ItemSpriteFactory.Instance.CreateArrow(),
                ItemSpriteFactory.Instance.CreateBlueRing(),
                ItemSpriteFactory.Instance.Create5Rupee(),
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
                ItemSpriteFactory.Instance.Create1Rupee(),
                ItemSpriteFactory.Instance.CreateTriforcePiece(),
                ItemSpriteFactory.Instance.CreateWhiteSword(),
                ItemSpriteFactory.Instance.CreateWoodBoomerang(),
                ItemSpriteFactory.Instance.CreateWoodShield(),
                ItemSpriteFactory.Instance.CreateWoodSword()
            };

            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);

            LinkSpriteFactory.Instance.LoadAllTextures(Content);

            Link = new Link(Vector2.Divide(_graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 2.0f));

            // Controller instanciation expects that IPlayer and IEnemy exist
            _controllers = new IController[]{
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

            foreach (var enemy in Enemies)
            {
                enemy.Update();
            }

            foreach (var block in _dungeonBorderBlocks)
            {
                block.Update();
            }

            foreach (var block in _dungeonEnvironmentBlocks)
            {
                block.Update();
            }

            foreach (var item in _items)
            {
                item.Update();
            }

            Link.Update();
            base.Update(gameTime);
        }

        private int DrawSpriteGrid(ISprite[] spriteArray, int columns, int y)
        {
            var rows = spriteArray.Length / columns;
            for (var row = 0; row < rows; row++)
            {
                var x = GraphicsDevice.Viewport.Bounds.Right / 2 - columns * 32 - 5;
                for (var col = 0; (rows * columns + col < spriteArray.Length) || col < columns; col++)
                {
                    spriteArray[row * 5 + col].Draw(new Vector2(x, y));
                    x += 32;
                }

                y += 32;
            }

            return rows * 32;
        }

        protected override void Draw(GameTime gameTime)
        {
            var y = 5;

            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f));

            y += DrawSpriteGrid(_items, 5, y);
            y += DrawSpriteGrid(_dungeonEnvironmentBlocks, 5, y);
            DrawSpriteGrid(_dungeonBorderBlocks, 5, y);

            Link.Draw();

            foreach (var enemy in Enemies)
            {
                enemy.Draw();
            }
            _spriteBatch.End();

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(5,5), Color.White);
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}