using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Music;
using Zelda.Player;
using Zelda.Projectiles;

namespace Zelda.Pause
{
    public class PauseMenu
    {
        private readonly Texture2D _image;
        private readonly SpriteBatch _spriteBatch;
        private IPlayer _player;
        private DungeonManager _dungeonManager;

        private Inventory _inventory = null;
        private int x;
        private int y;

        private ItemSpriteFactory _factory = ItemSpriteFactory.Instance;
        private PauseSpriteFactory _factory2 = PauseSpriteFactory.Instance;
        private ISprite _compass;
        private ISprite _arrow;
        private ISprite _bow;
        private ISprite _cursorGrid;
        private ISprite _playerMapDot;
        private ISprite _map;

        public bool Visible;

        public PauseMenu(SpriteBatch spriteBatch, ContentManager content, IPlayer player, DungeonManager dungeon)
        {
            _spriteBatch = spriteBatch;
            _image = content.Load<Texture2D>("PauseScreen");
            _player = player;
            _dungeonManager = dungeon;
            Visible = true;
            x = 0;
            y = 0;
            _inventory = _player.Inventory;

            _compass = _factory.CreateCompass();
            _map = _factory.CreateMap();
            _arrow = _factory.CreateArrow();
            _bow = _factory.CreateBow();

            _cursorGrid = _factory2.CreateCursorFrame();
            _playerMapDot = _factory2.CreateLinkIndicator();
        }

        public void Draw()
        {
            if (Visible)
            {
                _spriteBatch.Draw(_image, new Rectangle(0, 0, 512, 352), Color.White);

                _spriteBatch.End();

                _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f) * Matrix.CreateTranslation(0.0f, 96.0f, 0.0f));

                _cursorGrid.Draw(new Vector2(128 + (24 * x), -8 + (16 * y)));

                if (_inventory.HasMap)
                {
                    _map.Draw(new Vector2(47, 55));
                }
                if (_inventory.HasCompass)
                {
                    _compass.Draw(new Vector2(43, 95));
                }
                if (_inventory.HasArrow)
                {
                    //display arrow?? 
                }
                if (_inventory.HasBow)
                {
                    //display bow
                }
            }
        }

        public void unpause()
        {
            if(Visible == false)
            {
                Visible = true;
            } else
            {
                Visible = false;
            }
            System.Threading.Thread.Sleep(100);
        }

        public void selectUp()
        {
            if(y != 0)
            {
                y--;
            }
        }

        public void selectDown()
        {
            if(y != 1)
            {
                y++;
            }
        }

        public void selectLeft()
        {
            if (x != 0)
            {
                x--;
            }
        }

        public void selectRight()
        {
            if (x != 3)
            {
                x++;
            }
        }
    }
}

