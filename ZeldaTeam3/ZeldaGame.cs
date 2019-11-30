using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.HighScore;
using Zelda.HUD;
using Zelda.Items;
using Zelda.JumpMap;
using Zelda.Music;
using Zelda.Pause;
using Zelda.Player;
using Zelda.Projectiles;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;
using Zelda.GameState;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        private const int Width = 512;
        private const int Height = 448;

        private IGameStateAgent _survivalAgent;
        private IGameStateAgent _normalAgent;

        private bool _survivalMode = true;
        public IGameStateAgent GameStateAgent =>
            _survivalMode ? _survivalAgent : _normalAgent;

        private SpriteBatch _spriteBatch;

        public ZeldaGame()
        {
            // Use 2x size of NES window
            var graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Width, PreferredBackBufferHeight = Height,
            };
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprite.SpriteBatch = _spriteBatch;
            DrawnText.SpriteBatch = _spriteBatch;
            DrawnText.SpriteFont = Content.Load<SpriteFont>("prstartk");

            AlphaPassMask.SpriteBatch = _spriteBatch;
            var opaqueMaskTexture = new Texture2D(GraphicsDevice, 1, 1);
            opaqueMaskTexture.SetData(new[] { new Color(Color.White, 0.94f), });
            var transparentMaskTexture = new Texture2D(GraphicsDevice, 1, 1);
            transparentMaskTexture.SetData(new[] { Color.TransparentBlack });
            AlphaPassMask.OpaqueMaskTexture = opaqueMaskTexture;
            AlphaPassMask.TransparentMaskTexture = transparentMaskTexture;

            LightInTheDarkness.ShaderEffect = Content.Load<Effect>("LightInTheDarkness");
            LightInTheDarkness.GraphicsDevice = GraphicsDevice;
            LightInTheDarkness.SpriteBatch = _spriteBatch;

            PartyTime.ShaderEffect = Content.Load<Effect>("PartyTime");

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            ProjectileSpriteFactory.Instance.LoadAllTextures(Content);
            LinkSpriteFactory.Instance.LoadAllTextures(Content);
            BackgroundSpriteFactory.Instance.LoadAllTextures(Content);
            PauseSpriteFactory.Instance.LoadAllTextures(Content);
            HUDSpriteFactory.Instance.LoadAllTextures(Content);
            ScoreboardBackground.LoadTexture(Content);
            JumpMapScreen.LoadTexture(Content);
            MusicManager.Instance.LoadAllSounds(Content);
            SoundEffectManager.Instance.LoadAllSounds(Content);
            Survival.HUD.HUDSpriteFactory.Instance.LoadAllTextures(Content);
            Survival.Pause.PauseSpriteFactory.Instance.LoadAllTextures(Content);

            _normalAgent = new GameStateAgent(_spriteBatch, GraphicsDevice);
            _normalAgent.DungeonManager.LoadDungeonContent(Content);

            _survivalAgent = new Survival.GameState.GameStateAgent(_spriteBatch, GraphicsDevice);
            _survivalAgent.DungeonManager.LoadDungeonContent(Content);

            GameStateAgent.Reset();
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
        }

        protected override void Draw(GameTime gameTime)
        {
            GameStateAgent.Draw();

            base.Draw(gameTime);
        }
    }
}