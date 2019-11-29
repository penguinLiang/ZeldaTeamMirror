using System;
using Microsoft.Xna.Framework;

namespace Zelda.HighScore
{
    public class InitialEntryScreen : IDrawable
    {
        private const int ScreenWidth = 256;
        private const string EnterInitialsMessage = "ENTER YOUR INITIALS";
        private static readonly Point EnterInitialsLocation =
            new Point((ScreenWidth - DrawnText.Width(EnterInitialsMessage)) / 2, 64);
        private static readonly DrawnText EnterInitials = new DrawnText
            {Location = EnterInitialsLocation, Text = EnterInitialsMessage};

        private const string InitialsBlank = "___";
        private static readonly Point InitialsLocation =
            new Point((ScreenWidth - DrawnText.Width(InitialsBlank)) / 2, 80);
        private readonly DrawnText _initials = new DrawnText
            {Location = InitialsLocation, Text = InitialsBlank};

        private readonly Point _scoreLocation;
        private readonly DrawnText _score;

        private static readonly InitialEntryController EntryController = new InitialEntryController();
        public Action<string> OnSubmit {
            set => EntryController.OnSubmit = value;
        }

        public InitialEntryScreen(int score)
        {
            var scoreText = "SCORE: " + score;
            _scoreLocation = new Point((ScreenWidth - DrawnText.Width(scoreText)) / 2, 96);
            _score = new DrawnText { Location = _scoreLocation, Text = scoreText };
            EntryController.OnUpdate = HandleInitialsUpdate;
        }

        private void HandleInitialsUpdate(string initials)
        {
            _initials.Text = initials.PadRight(InitialsBlank.Length, '_');
        }

        public void Update()
        {
            EntryController.Update();
        }

        public void Draw()
        {
            EnterInitials.Draw();
            _initials.Draw();
            _score.Draw();
        }
    }
}
