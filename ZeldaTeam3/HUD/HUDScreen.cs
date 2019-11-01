using Microsoft.Xna.Framework;
using Zelda.GameState;

namespace Zelda.HUD
{
    public partial class HUDScreen : IDrawable
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;

        public HUDScreen(GameStateAgent agent, Point location)
        {
            _agent = agent;
            _location = location.ToVector2();
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
                switch (_agent.Player.SecondaryItem)
                {
                    case Items.Secondary.Bow:
                        return Bow;
                    case Items.Secondary.Boomerang:
                        return Boomerang;
                    case Items.Secondary.Bomb:
                        return Bomb;
                    default:
                        return null;
                }
            }
        }

        public void Update()
        {
            TriforceDot.Update();
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
            PlayerDot.Draw(MiniMapLocation + LinkLocation + _location);
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
        }
    }
}
