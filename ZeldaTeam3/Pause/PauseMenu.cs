using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zelda.Dungeon;
using Zelda.Items;
using Zelda.Player;
using Zelda.Commands;


namespace Zelda.Pause
{
    public class PauseMenu
    {
        private readonly Texture2D _background;
        private readonly SpriteBatch _spriteBatch;
        private IPlayer _player;
        private DungeonManager _dungeonManager;

        private Inventory _inventory;
        private int _cursorX;
        private int _cursorY;
        private bool _visible;

        private int CurrentRoomX => _dungeonManager.CurrentRoom.Y; // SHOULD EQUAL X
        private int CurrentRoomY => _dungeonManager.CurrentRoom.X; // SHOULD EQUAL Y

        public bool[][] _roomsUncovered { get; private set; }

        private ItemSpriteFactory _itemSpriteFactory = ItemSpriteFactory.Instance;
        private PauseSpriteFactory _pauseSpriteFactory = PauseSpriteFactory.Instance;
        private ISprite _compass;
        private ISprite _arrow;
        private ISprite _bow;
        private ISprite _cursorGrid;
        private ISprite _playerMapDot;
        private ISprite _map;
        private ISprite _boomerang;
        private ISprite _bomb;
        private ISprite _roomCover;

        public PauseMenu(SpriteBatch spriteBatch, ContentManager content, IPlayer player, DungeonManager dungeon)
        {
            _spriteBatch = spriteBatch;
            _background = content.Load<Texture2D>("PauseScreen");
            _player = player;
            _dungeonManager = dungeon;

            _roomsUncovered = new bool[6][];
            for(var i = 0; i < 6; i++)
            {
                _roomsUncovered[i] = new bool[6];
            }

            _inventory = _player.Inventory;

            _compass = _itemSpriteFactory.CreateCompass();
            _map = _itemSpriteFactory.CreateMap();
            _arrow = _itemSpriteFactory.CreateArrow();
            _bow = _itemSpriteFactory.CreateBow();
            _boomerang = _itemSpriteFactory.CreateWoodBoomerang();
            _bomb = _itemSpriteFactory.CreateBomb();

            _cursorGrid = _pauseSpriteFactory.CreateCursorFrame();
            _playerMapDot = _pauseSpriteFactory.CreateLinkIndicator();
            _roomCover = _pauseSpriteFactory.CreateMapCoverSquare();
        }

        public void Update()
        {
            if (_visible)
            {
                _cursorGrid.Update();
                _boomerang.Update();
                _bomb.Update();
                _arrow.Update();
                _bow.Update();
                _map.Update();
                _compass.Update();
                _roomCover.Update();
                _playerMapDot.Update();
            }

            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 6; col++)
                {
                    if (!_roomsUncovered[row][col] && col == CurrentRoomX && row == CurrentRoomY)
                        _roomsUncovered[row][col] = true;
                }
            }
        }

        public void Draw()
        {
            if (_visible)
            {
                _spriteBatch.Draw(_background, new Rectangle(0, -48, 256, 176), Color.White);

                _cursorGrid.Draw(new Vector2(128 + (24 * _cursorX), -8 + (16 * _cursorY)));

                if(!((CurrentRoomY == 1 && CurrentRoomX == 1) || (CurrentRoomY == 3 && CurrentRoomX == 5) || (CurrentRoomY == 4 && CurrentRoomX == 0) || (CurrentRoomY == 4 && CurrentRoomX == 5) || (CurrentRoomY == 5 && CurrentRoomX == 5)))
                {
                    _playerMapDot.Draw(new Vector2(136 + (CurrentRoomX * 8), 56 + (CurrentRoomY * 8)));
                }

                for(var row = 0; row < 6; row++)
                {
                    for(var col = 0; col < 6; col++)
                    {
                        if(!_roomsUncovered[row][col])
                            _roomCover.Draw(new Vector2(136 + (col * 8), 56 + (row * 8)));
                    }
                }

                if (_inventory.HasMap)
                {
                    _map.Draw(new Vector2(48, 52));
                }
                if (_inventory.HasCompass)
                {
                    _compass.Draw(new Vector2(44, 96));
                }
                if (_inventory.HasArrow)
                {
                    _arrow.Draw(new Vector2(176, -8));
                    if(_cursorX == 2 && _cursorY == 0)
                    {
                        _arrow.Draw(new Vector2(68, -8));
                    }
                }
                if (_inventory.HasBow)
                {
                    _bow.Draw(new Vector2(184, -8));
                }
                if (_inventory.HasBoomerang)
                {
                    _boomerang.Draw(new Vector2(132, -8));
                    if (_cursorX == 0 && _cursorY == 0)
                    {
                        _boomerang.Draw(new Vector2(68, -8));
                    }
                }
                if (_inventory.BombCount >= 1)
                {
                    _bomb.Draw(new Vector2(156, -8));
                    if (_cursorX == 1 && _cursorY == 0)
                    {
                        _bomb.Draw(new Vector2(68, -8));
                    }
                }

            }
        }

        public void unpause()
        {
            if(!_visible)
            {
                _visible = true;
            } else
            {
                _visible = false;
            }
        }

        private void assignSecondary()
        {
            if(_visible)
            {
                ICommand assign = new NoOp();
                if ((_cursorX == 0 && _cursorY == 0) && _inventory.HasBoomerang)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Boomerang);
                }
                if ((_cursorX == 0 && _cursorY == 1) && _inventory.BombCount >= 1)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Bomb);
                }
                if ((_cursorX == 0 && _cursorY == 2) && _inventory.HasBow)
                {
                    assign = new LinkSecondaryAssign(_player, Secondary.Bow);
                }
                assign.Execute();
            }
        }

        public void selectUp()
        {
            if(_cursorY != 0 && _visible)
            {
                _cursorY--;
            }
            assignSecondary();
        }

        public void selectDown()
        {
            if(_cursorY != 1 && _visible)
            {
                _cursorY++;
            }
            assignSecondary();
        }

        public void selectLeft()
        {
            if (_cursorX != 0 && _visible)
            {
                _cursorX--;
            }
            assignSecondary();
        }

        public void selectRight()
        {
            if (_cursorX != 3 && _visible)
            {
                _cursorX++;
            }
            assignSecondary();
        }
    }
}

