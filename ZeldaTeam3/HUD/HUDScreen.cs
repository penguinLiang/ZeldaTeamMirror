using Microsoft.Xna.Framework;
using Zelda.GameState;

namespace Zelda.HUD
{
    public partial class HUDScreen : IDrawable
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;
        private readonly DrawnText _rupeeCount = new DrawnText() { Location = RuppeeCountLocation };
        private readonly DrawnText _keyCount = new DrawnText() { Location = KeyCountLocation };
        private readonly DrawnText _bombCount = new DrawnText() { Location = BombCountLocation };

        public HUDScreen(GameStateAgent agent, Point location)
        {
            _agent = agent;
            _location = location.ToVector2();
            _rupeeCount.Location += location;
            _keyCount.Location += location;
            _bombCount.Location += location;
        }

        private Vector2 LinkLocation => _agent.DungeonManager.CurrentRoom.ToVector2() * MiniMapCellSize;

        private ISprite Primary
        {
            get
            {
                switch (_agent.Player.Inventory.SwordLevel)
                {
                    case Items.Primary.Sword:
                        return WoodSword;
                    case Items.Primary.WhiteSword:
                        return WhiteSword;
                    case Items.Primary.MagicalSword:
                        return MagicSword;
                    default:
                        return null;
                }
            }
        }

        private ISprite Secondary
        {
            get
            {
                switch (_agent.Player.Inventory.SecondaryItem)
                {
                    case Items.Secondary.Bow when _agent.Player.Inventory.HasBow && _agent.Player.Inventory.HasArrow:
                        return Arrow;
                    case Items.Secondary.Boomerang:
                        return Boomerang;
                    case Items.Secondary.Bomb:
                        return Bomb;
                    default:
                        return null;
                }
            }
        }

        private static string CountString(int count) => count > 100 ? count.ToString() : "X" + count;

        public void Update()
        {
            TriforceDot.Update();
            _rupeeCount.Text = CountString(_agent.Player.Inventory.RupeeCount);
            _bombCount.Text = CountString(_agent.Player.Inventory.BombCount);
            _keyCount.Text = CountString(_agent.Player.Inventory.KeyCount);
        }

        public void Draw()
        {
            Background.Draw(_location);

            if (_agent.Player.Inventory.HasMap)
            {
                MiniMap.Draw(MiniMapLocation + _location);
            }
            if (_agent.Player.Inventory.HasCompass)
            {
                TriforceDot.Draw(MiniMapLocation + TriforceLocation + _location);
            }
            if (_agent.DungeonManager.CurrentRoomMapped)
            {
                PlayerDot.Draw(MiniMapLocation + LinkLocation + _location);
            }

            Primary?.Draw(PrimaryLocation + _location);
            Secondary?.Draw(SecondaryLocation + _location);

            int i;
            for (i = 0; i < _agent.Player.Health / 2; i++)
            {
                Heart.Draw(HeartsLocation + _location + HeartOffset * i);
            }

            if (_agent.Player.Health % 2 != 0)
            {
                HalfHeart.Draw(HeartsLocation + _location + HeartOffset * i);
                i++;
            }

            for (; i < _agent.Player.MaxHealth / 2; i++)
            {
                EmptyHeart.Draw(HeartsLocation + _location + HeartOffset * i);
            }

            _rupeeCount.Draw();
            _bombCount.Draw();
            _keyCount.Draw();
        }
    }
}
