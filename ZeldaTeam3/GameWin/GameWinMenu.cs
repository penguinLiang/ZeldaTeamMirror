using Microsoft.Xna.Framework;
using Zelda.GameState;
using Zelda.HUD;

namespace Zelda.GameWin
{
    public class GameWinMenu : IMenu, IDrawable
    {
        private readonly GameStateAgent _agent;
        private const string GameWinMessage = "YOU WIN!";
        private const string RetryMessage = "Retry";
        private const string QuitMessage = "Quit";
        private const string PressEnter = "Press Enter to Select";

        private static readonly Point GameWinMessageLocation =
            new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(GameWinMessage)) / 2, 0);

        private const int ScreenHeight = 224;
        private const int ScreenWidth = 256;

        private static readonly Point RetryMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameWinMessage)) / 4, ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameWinMessage)) / 4, ScreenHeight - 120);
        private static readonly Point PressEnterLocation = new Point((ScreenWidth - DrawnText.Width(PressEnter)) / 4, ScreenHeight - 80);

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText { Location = GameWinMessageLocation, Text = GameWinMessage },
            new DrawnText { Location = RetryMessageLocation, Text = RetryMessage },
            new DrawnText { Location = QuitMessageLocation, Text = QuitMessage },
            new DrawnText { Location = PressEnterLocation, Text = PressEnter }
        };

        private Vector2 _location;
        private string _selectedItem;

        private readonly ISprite _cursor;

        public GameWinMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = RetryMessage;
            _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
            _cursor = HUDSpriteFactory.Instance.CreateFullHeart();
        }

        public void Choose()
        {
            if (_selectedItem == RetryMessage)
            {
                _agent.Reset();
            }
            else
            {
                _agent.Quit();
            }
        }

        public void SelectUp()
        {
            _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
            _selectedItem = RetryMessage;
        }
        public void SelectDown()
        {
            _location = new Vector2(QuitMessageLocation.X - 16, QuitMessageLocation.Y);
            _selectedItem = QuitMessage;
        }

        public void SelectLeft()
        {
            //no op
        }
        public void SelectRight()
        {
            //no op
        }

        public void Draw()
        {
            foreach (var textDrawable in _textDrawables)
            {
                textDrawable.Draw();
            }
            _cursor.Draw(_location);
        }

        public void Update()
        {
            //no op
        }
    }
}
