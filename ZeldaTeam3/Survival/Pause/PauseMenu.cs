using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Survival.GameState;
using Zelda.Items;

// ReSharper disable ConvertIfStatementToSwitchStatement
namespace Zelda.Survival.Pause
{
    public partial class PauseMenu : IDrawable, IMenu
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;
        private Point _cursorPosition;
        private ISprite _selectedItem;
        private ISprite _slot7Sprite;
        private ISprite _slot8Sprite;

        private static ISprite GetExtraItemSprite(Secondary extraItem)
        {
            switch (extraItem)
            {
                case Secondary.LaserBeam:
                    return LaserBeam;
                case Secondary.Clock:
                    return Clock;
                case Secondary.Star:
                    return Star;
                case Secondary.Bait:
                    return Bait;
                default:
                    return null;
            }
        }

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
                case Secondary.ExtraSlot1:
                    _selectedItem = GetExtraItemSprite(_agent.Player.Inventory.ExtraItem1);
                    _cursorPosition = Slot7Position;
                    break;
                case Secondary.ExtraSlot2:
                    _selectedItem = GetExtraItemSprite(_agent.Player.Inventory.ExtraItem2);
                    _cursorPosition = Slot8Position;
                    break;
                case Secondary.None:
                    break;
                case Secondary.Arrow:
                    break;
                case Secondary.SilverArrow:
                    break;
                case Secondary.LaserBeam:
                    break;
                case Secondary.Bait:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ISprite UpdateExtraItemSprite(Secondary item)
        {
            switch (item)
            {
                case Secondary.LaserBeam:
                    return LaserBeam;
                case Secondary.Clock:
                    return Clock;
                case Secondary.Star:
                    return Star;
                case Secondary.Bait:
                    return Bait;
                default:
                    return null;
            }
        }

        public void Update()
        {
            _slot7Sprite = UpdateExtraItemSprite(_agent.Player.Inventory.ExtraItem1);
            _slot8Sprite = UpdateExtraItemSprite(_agent.Player.Inventory.ExtraItem2);
            CursorGrid.Update();
        }

        private void DrawExtraItem(ISprite sprite, Vector2 location8_16)
        {
            Vector2 drawLocation = new Vector2(location8_16.X, location8_16.Y);
            if (sprite != null && sprite.Width > 8)
                drawLocation.X -= 4;

            sprite?.Draw(drawLocation + GridLocation + _location);
        }

        public void Draw()
        {
            Background.Draw(_location);
            if (_selectedItem != null) {
                if (_selectedItem.Width > 8)
                    _selectedItem.Draw(_location + SelectedItemLocation16_16);
                else
                    _selectedItem.Draw(_location + SelectedItemLocation8_16);
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

            if (_agent.Player.Inventory.Coins >= 2)
                AlchemyCoin.Draw(CoinLocation + GridLocation + _location);

            if (_agent.Player.Inventory.HasATWBoomerang)
                ATWBoomerang.Draw(ATWBoomerangLocation + GridLocation + _location);

            if (_agent.Player.Inventory.HasBombLauncher)
                BombLauncher.Draw(BombLauncherLocation + GridLocation + _location);

            DrawExtraItem(_slot7Sprite, Slot7Location);
            DrawExtraItem(_slot8Sprite, Slot8Location);

            CursorGrid.Draw(CursorSize * _cursorPosition.ToVector2() + GridLocation + _location);
        }

        private void AssignSecondary()
        {
            Secondary item;
            if (_cursorPosition == BoomerangPosition && _agent.Player.Inventory.HasBoomerang)
            {
                item = Secondary.Boomerang;
                _selectedItem = Boomerang;
            }
            else if (_cursorPosition == BombPosition && _agent.Player.Inventory.BombCount >= 1)
            {
                item = Secondary.Bomb;
                _selectedItem = Bomb;
            }
            else if (_cursorPosition == BowPosition && _agent.Player.Inventory.BowLevel != Secondary.None
                && _agent.Player.Inventory.ArrowLevel != Secondary.None)
            {
                item = _agent.Player.Inventory.BowLevel == Secondary.Bow ? Secondary.Bow : Secondary.FireBow;
                _selectedItem = _agent.Player.Inventory.ArrowLevel == Secondary.Arrow ? Arrow : SilverArrow;
            }
            else if (_cursorPosition == CoinPosition && _agent.Player.Inventory.Coins >= 2)
            {
                item = Secondary.Coins;
                _selectedItem = AlchemyCoin;
            }
            else if (_cursorPosition == ATWBoomerangPosition && _agent.Player.Inventory.HasATWBoomerang)
            {
                item = Secondary.ATWBoomerang;
                _selectedItem = ATWBoomerang;
            }
            else if (_cursorPosition == BombLauncherPosition && _agent.Player.Inventory.HasBombLauncher)
            {
                item = Secondary.BombLauncher;
                _selectedItem = BombLauncher;
            }
            else if (_cursorPosition == Slot7Position)
            {
                item = Secondary.ExtraSlot1;
                _selectedItem = _slot7Sprite;
            }
            else if (_cursorPosition == Slot8Position)
            {
                item = Secondary.ExtraSlot2;
                _selectedItem = _slot8Sprite;
            }
            else
            {
                item = Secondary.None;
                _selectedItem = null;
            }
            _agent.Player.AssignSecondaryItem(item);
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

