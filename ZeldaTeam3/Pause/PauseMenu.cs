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
                case Secondary.Bow when agent.Player.Inventory.ArrowLevel != Secondary.None:
                case Secondary.FireBow when agent.Player.Inventory.ArrowLevel != Secondary.None:
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
                case Secondary.FireBow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            switch (_agent.Player.Inventory.ExtraItem1)
            {
                case Secondary.WideBeam:
                    _slot7Sprite = WideBeam;
                    break;
                default:
                    _slot7Sprite = null;
                    break;
            }
            switch (_agent.Player.Inventory.ExtraItem2)
            {
                case Secondary.WideBeam:
                    _slot8Sprite = WideBeam;
                    break;
                default:
                    _slot8Sprite = null;
                    break;
            }
            CursorGrid.Update();
        }

        public void Draw()
        {
            Background.Draw(_location);
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
            if (_agent.Player.Inventory.ArrowLevel == Secondary.SilverArrow)
                SilverArrow.Draw(ArrowLocation + GridLocation + _location);
            else if (_agent.Player.Inventory.ArrowLevel == Secondary.Arrow)
                Arrow.Draw(ArrowLocation + GridLocation + _location);
            if (_agent.Player.Inventory.BowLevel == Secondary.FireBow)
                FireBow.Draw(BowLocation + GridLocation + _location);
            else if (_agent.Player.Inventory.BowLevel == Secondary.Bow)
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

            CursorGrid.Draw(CursorSize * _cursorPosition.ToVector2() + GridLocation + _location);
        }

        private void AssignSecondary()
        {
            ICommand assign = new NoOp();
            if (_cursorPosition == BoomerangPosition && _agent.Player.Inventory.HasBoomerang)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Boomerang);
                _selectedItem = Boomerang;
            }
            else if (_cursorPosition == BombPosition && _agent.Player.Inventory.BombCount >= 1)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Bomb);
                _selectedItem = Bomb;
            }
            else if (_cursorPosition == BowPosition && _agent.Player.Inventory.BowLevel != Secondary.None
                && _agent.Player.Inventory.ArrowLevel != Secondary.None)
            {
                assign = new LinkSecondaryAssign(_agent.Player,
                    _agent.Player.Inventory.BowLevel == Secondary.Bow ? Secondary.Bow : Secondary.FireBow);
                _selectedItem = _agent.Player.Inventory.ArrowLevel == Secondary.Arrow ? Arrow : SilverArrow;
            }
            else if (_cursorPosition == CoinPosition && _agent.Player.Inventory.HasCoins)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.Coins);
                _selectedItem = AlchemyCoin;
            }
            else if (_cursorPosition == ATWBoomerangPosition && _agent.Player.Inventory.HasATWBoomerang)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.ATWBoomerang);
                _selectedItem = ATWBoomerang;
            }
            else if (_cursorPosition == BombLauncherPosition && _agent.Player.Inventory.HasBombLauncher)
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.BombLauncher);
                _selectedItem = BombLauncher;
            }
            else if (_cursorPosition == Slot7Position)
            {
                assign = new LinkSecondaryAssign(_agent.Player, _agent.Player.Inventory.ExtraItem1);
                _selectedItem = _slot7Sprite;
            }
            else if (_cursorPosition == Slot8Position)
            {
                assign = new LinkSecondaryAssign(_agent.Player, _agent.Player.Inventory.ExtraItem2);
                _selectedItem = _slot8Sprite;
            }
            else
            {
                assign = new LinkSecondaryAssign(_agent.Player, Secondary.None);
                _selectedItem = null;
            }
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

