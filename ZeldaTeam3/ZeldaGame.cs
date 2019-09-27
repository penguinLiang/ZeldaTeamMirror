using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                PreferredBackBufferWidth = 1200, PreferredBackBufferHeight = 900
            };
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Arial");

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            Enemies = new IEnemy[]
            {
                new Gel(_spriteBatch, 500, 600),
                new Goriya(_spriteBatch, 550, 600), 
                new Keese(_spriteBatch, 600, 600),
                new Stalfos(_spriteBatch, 650, 600),
                new Trap(_spriteBatch, 700, 600), 
                new WallMaster(_spriteBatch, 750, 600),
                new Aquamentus(_spriteBatch, 800, 600),
                new OldMan(_spriteBatch, 450, 600)
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
                BlockSpriteFactory.Instance.CreateGapTile(),
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

            Link = new Link(_spriteBatch, _graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2());

            // Controller instanciation expects that IPlayer and IEnemy exist
            _controllers = new IController[]{
                new ControllerKeyboard(this)
            };

            foreach (var controller in _controllers)
            {
                _controlsDescription += controller + "\n";
            }
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

        private void DrawSpriteGrid(ISprite[] spriteArray, int x, int y)
        {
            foreach (var sprite in spriteArray)
            {
                if (x < GraphicsDevice.Viewport.Bounds.Right && y < GraphicsDevice.Viewport.Bounds.Bottom)
                {
                    x += 32;
                }
                else
                {
                    x = GraphicsDevice.Viewport.Bounds.Center.X + 32;
                    y += 32;
                }

                sprite.Draw(_spriteBatch, new Vector2(x, y));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var xBorderBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            const int yBorderBlocks = 300;
            var xEnvironmentBlocks = GraphicsDevice.Viewport.Bounds.Center.X - 100;
            const int yEnvironmentBlocks = 400;
            var xItems = GraphicsDevice.Viewport.Bounds.Center.X;
            const int yItems = 50;
            
            _spriteBatch.Begin();

            DrawSpriteGrid(_dungeonBorderBlocks, xBorderBlocks, yBorderBlocks);
            DrawSpriteGrid(_dungeonEnvironmentBlocks, xEnvironmentBlocks, yEnvironmentBlocks);
            DrawSpriteGrid(_items, xItems, yItems);

            Link.Draw();

            foreach (var enemy in Enemies)
            {
                enemy.Draw();
            }

            _spriteBatch.DrawString(_font, _controlsDescription, new Vector2(0,0), Color.White);
          
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}