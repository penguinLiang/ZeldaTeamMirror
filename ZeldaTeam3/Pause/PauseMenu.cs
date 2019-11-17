using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.GameState;
using Zelda.Items;

namespace Zelda.Pause
{
    public partial class PauseMenu : IDrawable, IMenu
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;
        private Point _cursorPosition;
        private ISprite _selectedItem;
        private ISprite _slot7Sprite;
        private ISprite _slot8Sprite;

        public PauseMenu(GameStateAgent agent, Point location)
        {
            _agent = agent;
            _location = location.ToVector2();
            switch (agent.Player.Inventory.SecondaryItem)
            {
                case Secondary.Boomerang:
                    _selectedItem = Boomerang;
                    _cursorPosition = BoomerangPosition;
                    break;
                case Secondary.Bomb:
                    _selectedItem = Bomb;
                    _cursorPosition = BombPosition;
                    break;
                case Secondary.Bow when agent.Player.Inventory.ArrowLevel != Secondary.None
                && agent.Player.Inventory.BowLevel != Secondary.None:
                    _selectedItem = Arrow;
                    _cursorPosition = BowPosition;
                    break;
                case Secondary.Coins:
                    _selectedItem = AlchemyCoin;
                    _cursorPosition = CoinPosition;
                    break;
                case Secondary.ATWBoomerang:
                    _selectedItem = ATWBoomerang;
                    _cursorPosition = ATWBoomerangPosition;
                    break;
                case Secondary.BombLauncher:
                    _selectedItem = BombLauncher;
                    _cursorPosition = BombLauncherPosition;
                    break;
                case Secondary.None:
                case Secondary.Bow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            CursorGrid.Update();
        }

        public void Draw()
        {
            Background.Draw(_location);
            CursorGrid.Draw(CursorSize * _cursorPosition.ToVector2() + GridLocation + _location);
            _selectedItem?.Draw(_location + SelectedItemLocation);

            var currentRoom = _agent.DungeonManager.CurrentRoom.ToVector2();
            var visitedRooms = _agent.DungeonManager.VisitedRooms;
            if (_agent.DungeonManager.CurrentRoomMapped)
            {
                PlayerMapDot.Draw(MapGridCoverSize * currentRoom + MapGridLocation + _location);
            }

            for (var row = 0; row < visitedRooms.Length; row++)
            {
                for (var col = 0; col < visitedRooms[row].Length; col++)
                {
                    if (!visitedRooms[row][col])
                        RoomCover.Draw(MapGridCoverSize * new Vector2(col, row) + MapGridLocation + _location);
                }
            }

            if (_agent.Player.Inventory.HasBoomerang)
                Boomerang.Draw(BoomerangLocation + GridLocation + _location);
            if (_agent.Player.Inventory.BombCount >= 1)
                Bomb.Draw(BombLocation + GridLocation + _location);
            if (_agent.Player.Inventory.ArrowLevel != Secondary.None)
                Arrow.Draw(ArrowLocation + GridLocation + _location);
            if (_agent.Player.Inventory.BowLevel != Secondary.None)
                Bow.Draw(BowLocation + GridLocation + _location);
            if (_agent.Player.Inventory.HasCoins)
                AlchemyCoin.Draw(CoinLocation + GridLocation + _location);
            if (_agent.Player.Inventory.HasATWBoomerang)
                ATWBoomerang.Draw(ATWBoomerangLocation + GridLocation + _location);
            if (_agent.Player.Inventory.HasBombLauncher)
                BombLauncher.Draw(BombLauncherLocation + GridLocation + _location);

            if (_agent.Player.Inventory.HasMap)
                Map.Draw(MapLocation + _location);
            if (_agent.Player.Inventory.HasCompass)
                Compass.Draw(CompassLocation + _location);
        }

        private void AssignSecondary()
        {
            ICommand assign = new NoOp();
            if (_cursorPosition == BoomerangPosition && _agent.Player.Inventory.HasBoomerang)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Boomerang);
                _selectedItem = Boomerang;
            }
            if (_cursorPosition == BombPosition && _agent.Player.Inventory.BombCount >= 1)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Bomb);
                _selectedItem = Bomb;
            }
            if (_cursorPosition == BowPosition && _agent.Player.Inventory.BowLevel != Secondary.None
                && _agent.Player.Inventory.ArrowLevel != Secondary.None)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Bow);
                _selectedItem = Arrow;
            }
            /* 
             * Refactor to allow selecting none and maybe look neater; also needs to work for new weapons
             */
            assign.Execute();
        }

        public void Choose()
        {
            // NO-OP: Pause menu selects instantly
        }

        public void SelectUp()
        {
            _cursorPosition.Y = Math.Max(0, _cursorPosition.Y - 1);
            AssignSecondary();
        }

        public void SelectDown()
        {
            _cursorPosition.Y = Math.Min(InventoryGridRows - 1, _cursorPosition.Y + 1);
            AssignSecondary();
        }

        public void SelectLeft()
        {
            _cursorPosition.X = Math.Max(0, _cursorPosition.X - 1);
            AssignSecondary();
        }

        public void SelectRight()
        {
            _cursorPosition.X = Math.Min(InventoryGridColumns - 1, _cursorPosition.X + 1);
            AssignSecondary();
        }
    }
}

