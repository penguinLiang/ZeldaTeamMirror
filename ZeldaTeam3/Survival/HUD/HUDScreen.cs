using Microsoft.Xna.Framework;
using Zelda.Survival.GameState;

namespace Zelda.Survival.HUD
{
    public partial class HUDScreen : IDrawable
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;
        private readonly DrawnText _rupeeCount = new DrawnText { Location = RuppeeCountLocation };
        private readonly DrawnText _keyCount = new DrawnText { Location = KeyCountLocation };
        private readonly DrawnText _bombCount = new DrawnText { Location = BombCountLocation };
        private readonly DrawnText _score = new DrawnText { Location = new Point(18, 24), Text = "X000000" };

        public HUDScreen(GameStateAgent agent, Point location)
        {
            _agent = agent;
            _location = location.ToVector2();
            _rupeeCount.Location += location;
            _keyCount.Location += location;
            _bombCount.Location += location;
            _score.Location += location;
        }

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
                    case Items.Secondary.Boomerang:
                        return Boomerang;
                    case Items.Secondary.Bomb:
                        return Bomb;
                    case Items.Secondary.Bow when _agent.Player.Inventory.ArrowLevel != Items.Secondary.None:
                    case Items.Secondary.FireBow when _agent.Player.Inventory.ArrowLevel != Items.Secondary.None:
                        return _agent.Player.Inventory.ArrowLevel == Items.Secondary.Arrow ? Arrow : SilverArrow;
                    case Items.Secondary.Coins:
                        return AlchemyCoin;
                    case Items.Secondary.ATWBoomerang:
                        return ATWBoomerang;
                    case Items.Secondary.BombLauncher:
                        return BombLauncher;
                    case Items.Secondary.ExtraSlot1:
                        return GetExtraItemSprite(_agent.Player.Inventory.ExtraItem1);
                    case Items.Secondary.ExtraSlot2:
                        return GetExtraItemSprite(_agent.Player.Inventory.ExtraItem2);
                    default:
                        return null;
                }
            }
        }

        private static ISprite GetExtraItemSprite(Items.Secondary extraItem)
        {
            switch (extraItem)
            {
                case Items.Secondary.LaserBeam:
                    return LaserBeam;
                case Items.Secondary.Clock:
                    return Clock;
                case Items.Secondary.Star:
                    return Star;
                case Items.Secondary.Bait:
                    return Bait;
                default:
                    return null;
            }
        }

        private static string CountString(int count) => count >= 100 ? count.ToString() : "X" + count;

        public void Update()
        {
            _rupeeCount.Text = CountString(_agent.Player.Inventory.RupeeCount);
            _bombCount.Text = CountString(_agent.Player.Inventory.BombCount);
            _keyCount.Text = CountString(_agent.Player.Inventory.KeyCount);
            _score.Text = "X" + _agent.Score.ToString("D6");
        }

        public void Draw()
        {
            Background.Draw(_location);
            
            var inv = _agent.Player.Inventory;

            Primary?.Draw(PrimaryLocation + _location);
            switch (inv.SecondaryItem)
            {
                case Items.Secondary.Coins:
                case Items.Secondary.ExtraSlot1 when inv.ExtraItem1 == Items.Secondary.Clock || inv.ExtraItem1 == Items.Secondary.Star:
                case Items.Secondary.ExtraSlot2 when inv.ExtraItem2 == Items.Secondary.Clock || inv.ExtraItem2 == Items.Secondary.Star:
                    Secondary?.Draw(SecondaryLocation16_16 + _location);
                    break;
                default:
                    Secondary?.Draw(SecondaryLocation8_16 + _location);
                    break;
            }

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
            _score.Draw();
        }
    }
}
