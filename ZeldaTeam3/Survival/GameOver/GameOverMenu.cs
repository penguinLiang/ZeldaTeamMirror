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

        private const string SubmitScoreMessage = "SUBMIT SCORE";
        private const string RetryMessage = "RETRY";
        private const string QuitMessage = "QUIT";
        private const string PressEnter = "PRESS ENTER TO SELECT";

        private static readonly Point SubmitScoreMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 160);
        private static readonly Point RetryMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((ScreenWidth - DrawnText.Width(GameOverMessage)) / 4, ScreenHeight - 120);
        private static readonly Point PressEnterLocation = new Point((ScreenWidth - DrawnText.Width(PressEnter)) / 4, ScreenHeight - 80);

        private readonly DrawnText _score = new DrawnText();

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText { Location = GameOverMessageLocation, Text = GameOverMessage },
            new DrawnText { Location = SubmitScoreMessageLocation, Text = SubmitScoreMessage },
            new DrawnText { Location = RetryMessageLocation, Text = RetryMessage },
            new DrawnText { Location = QuitMessageLocation, Text = QuitMessage },
            new DrawnText { Location = PressEnterLocation, Text = PressEnter },
        };

        private readonly GameStateAgent _agent;
        private Vector2 _location;
        private string _selectedItem;

        private readonly ISprite _cursor;

        public GameOverMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = SubmitScoreMessage;
            _location = new Vector2(SubmitScoreMessageLocation.X - 16, SubmitScoreMessageLocation.Y);
            _cursor = HUDSpriteFactory.Instance.CreateFullHeart();
            var scoreMessage = "SCORE: " + agent.Score.ToString("D6");
            _score.Location = new Point((ScreenWidth - DrawnText.Width(scoreMessage)) / 2, 16);
            _score.Text = scoreMessage;
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
            _score.Draw();
        }

        public void Choose()
        {
            switch (_selectedItem)
            {
                case SubmitScoreMessage:
                    _agent.SubmitScore();
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
                    _selectedItem = SubmitScoreMessage;
                    _location = new Vector2(SubmitScoreMessageLocation.X - 16, SubmitScoreMessageLocation.Y);
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
                case SubmitScoreMessage:
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
