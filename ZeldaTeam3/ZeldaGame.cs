﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Survival.GameState;
using Zelda.HighScore;
using Zelda.HUD;
using Zelda.Items;
using Zelda.JumpMap;
using Zelda.Music;
using Zelda.Pause;
using Zelda.Player;
using Zelda.Projectiles;
using Zelda.SoundEffects;

namespace Zelda
{
    public class ZeldaGame : Game
    {
        public GameStateAgent GameStateAgent { get; private set; }

        private SpriteBatch _spriteBatch;

        public ZeldaGame()
        {
            // Use 2x size of NES window
            var graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 512, PreferredBackBufferHeight = 448
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
            SoundEffectManager.Instance.LoadAllSounds(Content);
            Survival.HUD.HUDSpriteFactory.Instance.LoadAllTextures(Content);
            Survival.Pause.PauseSpriteFactory.Instance.LoadAllTextures(Content);

            GameStateAgent = new GameStateAgent(_spriteBatch);
            GameStateAgent.DungeonManager.LoadDungeonContent(Content);
            GameStateAgent.Reset();

            try
            {
                //HighScoreClient.Submit(new PlayerScore {Initials = "RS3", Score = 1337});
                foreach (var score in HighScoreClient.Scores())
                {
                    Console.WriteLine(score.Initials + " " + score.Score);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("OH NO! Could not get the scores!");
            }
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
            GraphicsDevice.Clear(Color.Black);
            GameStateAgent.Draw();
            base.Draw(gameTime);
        }
    }
}