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
using Zelda.Commands;


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

        public bool[][] _roomsUncovered { get; private set; }

        private ItemSpriteFactory _factory = ItemSpriteFactory.Instance;
        private PauseSpriteFactory _factory2 = PauseSpriteFactory.Instance;
        private ISprite _compass;
        private ISprite _arrow;
        private ISprite _bow;
        private ISprite _cursorGrid;
        private ISprite _playerMapDot;
        private ISprite _map;
        private ISprite _boomerang;
        private ISprite _bomb;
        private ISprite _roomCover;

        public bool Visible;

        public PauseMenu(SpriteBatch spriteBatch, ContentManager content, IPlayer player, DungeonManager dungeon)
        {
            _spriteBatch = spriteBatch;
            _image = content.Load<Texture2D>("PauseScreen");
            _player = player;
            _dungeonManager = dungeon;
            Visible = true;
            _roomsUncovered = new bool[6][];

            for(var i = 0; i < 6; i++)
            {
                _roomsUncovered[i] = new bool[6];
            }

            x = 0;
            y = 0;
            _inventory = _player.Inventory;

            _compass = _factory.CreateCompass();
            _map = _factory.CreateMap();
            _arrow = _factory.CreateArrow();
            _bow = _factory.CreateBow();
            _boomerang = _factory.CreateWoodBoomerang();
            _bomb = _factory.CreateBomb();

            _cursorGrid = _factory2.CreateCursorFrame();
            _playerMapDot = _factory2.CreateLinkIndicator();
            _roomCover = _factory2.CreateMapCoverSquare();

            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 6; col++)
                {
                    _roomsUncovered[col][row] = false;
                }
            }
        }

        public void Draw()
        {
            if (Visible)
            {
                _spriteBatch.End();

                _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.0f) * Matrix.CreateTranslation(0.0f, 96.0f, 0.0f));

                _spriteBatch.Draw(_image, new Rectangle(0, -48, 256, 176), Color.White);

                _cursorGrid.Draw(new Vector2(128 + (24 * x), -8 + (16 * y)));

                int _roomX = _dungeonManager.CurrentRoom.X;
                int _roomY = _dungeonManager.CurrentRoom.Y;
                if(!((_roomX == 1 && _roomY == 1) || (_roomX == 3 && _roomY == 5) || (_roomX == 4 && _roomY == 5) || (_roomX == 5 && _roomY == 5) || (_roomX == 4 && _roomY == 0)))
                {
                    _playerMapDot.Draw(new Vector2(136 + (_roomY * 8), 56 + (_roomX * 8)));
                }

                for(var row = 0; row < 6; row++)
                {
                    for(var col = 0; col < 6; col++)
                    {
                        if(col == _roomY &&  row == _roomX && _roomsUncovered[col][row] == false)
                        {
                            _roomsUncovered[col][row] = true;
                        }
                        if(_roomsUncovered[col][row] != true)
                        {
                            _roomCover.Draw(new Vector2(136 + (col * 8), 56 + (row * 8)));
                        }
                    }
                }
                

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
                    _arrow.Draw(new Vector2(128 + 48, -8));
                    if(x == 2 && y == 0)
                    {
                        _arrow.Draw(new Vector2(68, -8));
                    }
                }
                if (_inventory.HasBow)
                {
                    _bow.Draw(new Vector2(128 + 56, -8));
                }
                if (_inventory.HasBoomerang)
                {
                    _boomerang.Draw(new Vector2(128 + 3, -8));
                    if (x == 0 && y == 0)
                    {
                        _boomerang.Draw(new Vector2(68, -8));
                    }
                }
                if (_inventory.BombCount >= 1)
                {
                    _bomb.Draw(new Vector2(128 + 27, -8));
                    if (x == 1 && y == 0)
                    {
                        _bomb.Draw(new Vector2(68, -8));
                    }
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
        }

        private void assignSecondary()
        {
            if(Visible)
            {
                ICommand assign = new NoOp();
                if ((x == 0 && y == 0) && _inventory.HasBoomerang)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Boomerang);
                }
                if ((x == 0 && y == 1) && _inventory.BombCount >= 1)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Bomb);
                }
                if ((x == 0 && y == 2) && _inventory.HasBow)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Bow);
                }
                assign.Execute();
            }
        }

        public void selectUp()
        {
            if(y != 0 && Visible)
            {
                y--;
            }
            assignSecondary();
        }

        public void selectDown()
        {
            if(y != 1 && Visible)
            {
                y++;
            }
            assignSecondary();
        }

        public void selectLeft()
        {
            if (x != 0 && Visible)
            {
                x--;
            }
            assignSecondary();
        }

        public void selectRight()
        {
            if (x != 3 && Visible)
            {
                x++;
            }
            assignSecondary();
        }
    }
}

