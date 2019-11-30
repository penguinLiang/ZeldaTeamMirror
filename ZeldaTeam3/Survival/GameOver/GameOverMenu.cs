using Microsoft.Xna.Framework;
using Zelda.Survival.GameState;
using Zelda.Survival.HUD;

namespace Zelda.Survival.GameOver
{
    public class GameOverMenu : IDrawable, IMenu
    {
        private const string GameOverMessage = "GAME OVER";
        private const int ScreenHeight = Dimensions.ScreenHeight;
        private const int ScreenWidth = Dimensions.ScreenWidth;
        private static readonly Point GameOverMessageLocation =
            new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 2, 0);

        private const string ContinueMessage = "CONTINUE";
        private const string RetryMessage = "RETRY";
        private const string QuitMessage = "QUIT";
        private const string PressEnter = "PRESS ENTER TO SELECT";

        private static readonly Point ContinueMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 160);
        private static readonly Point RetryMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 120);
        private static readonly Point PressEnterLocation = new Point((ScreenWidth - DrawnText.Width(PressEnter)) / 4, ScreenHeight - 80);

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText { Location = GameOverMessageLocation, Text = GameOverMessage },
            new DrawnText { Location = ContinueMessageLocation, Text = ContinueMessage },
            new DrawnText { Location = RetryMessageLocation, Text = RetryMessage },
            new DrawnText { Location = QuitMessageLocation, Text = QuitMessage },
            new DrawnText { Location = PressEnterLocation, Text = PressEnter }
        };

        private readonly GameStateAgent _agent;
        private Vector2 _location;
        private string _selectedItem;

        private readonly ISprite _cursor;

        public GameOverMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = ContinueMessage;
            _location = new Vector2(ContinueMessageLocation.X - 16, ContinueMessageLocation.Y);
            _cursor = HUDSpriteFactory.Instance.CreateFullHeart();
        }

        public void Update()
        {
            // NO-OP
        }

        public void Draw()
        {
            foreach (var textDrawable in _textDrawables)
            {
                textDrawable.Draw();
            }
            _cursor.Draw(_location);
        }

        public void Choose()
        {
            switch (_selectedItem)
            {
                case ContinueMessage:
                    _agent.Continue();
                    break;
                case RetryMessage:
                    _agent.Reset();
                    break;
                default:
                    _agent.Quit();
                    break;
            }
        }

        public void SelectUp()
        {
            // ReSharper disable once SwitchStatementMissingSomeCases (default is a no-op)
            switch (_selectedItem)
            {
                case QuitMessage:
                    _selectedItem = RetryMessage;
                    _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
                    break;
                case RetryMessage:
                    _selectedItem = ContinueMessage;
                    _location = new Vector2(ContinueMessageLocation.X - 16, ContinueMessageLocation.Y);
                    break;
            }
        }

        public void SelectDown()
        {
            // ReSharper disable once SwitchStatementMissingSomeCases (default is a no-op)
            switch (_selectedItem)
            {
                case RetryMessage:
                    _selectedItem = QuitMessage;
                    _location = new Vector2(QuitMessageLocation.X - 16, QuitMessageLocation.Y);
                    break;
                case ContinueMessage:
                    _selectedItem = RetryMessage;
                    _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
                    break;
            }
        }

        public void SelectLeft()
        {
            //no op
        }

        public void SelectRight()
        {
            //no op
        }
    }
}
