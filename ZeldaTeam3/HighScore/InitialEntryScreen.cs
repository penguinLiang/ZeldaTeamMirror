using System;
using Microsoft.Xna.Framework;

namespace Zelda.HighScore
{
    public class InitialEntryScreen : IDrawable
    {
        private const int ScreenWidth = 256;
        private const string EnterInitialsMessage = "ENTER YOUR INITIALS";
        private static readonly Point EnterInitialsLocation =
            new Point((ScreenWidth - DrawnText.Width(EnterInitialsMessage)) / 2, 16);
        private static readonly DrawnText EnterInitials = new DrawnText
            {Location = EnterInitialsLocation, Text = EnterInitialsMessage};

        private const string InitialsBlank = "___";
        private static readonly Point InitialsLocation =
            new Point((ScreenWidth - DrawnText.Width(InitialsBlank)) / 2, 32);
        private readonly DrawnText _initials = new DrawnText
            {Location = InitialsLocation, Text = InitialsBlank};

        private const string Instruction1Message = "TYPE USING LETTERS AND NUMBERS";
        private const string Instruction2Message = "PRESS ENTER TO SUBMIT";

        private readonly DrawnText _instruction1 = new DrawnText
            { Location = new Point((ScreenWidth - DrawnText.Width(Instruction1Message)) / 2, 80), Text = Instruction1Message };
        private readonly DrawnText _instruction2 = new DrawnText
            { Location = new Point((ScreenWidth - DrawnText.Width(Instruction2Message)) / 2, 88), Text = Instruction2Message };

        private readonly DrawnText _score;

        private readonly InitialEntryController _entryController = new InitialEntryController();
        public Action<string> OnSubmit {
            set => _entryController.OnSubmit = value;
        }

        public InitialEntryScreen(int score)
        {
            var scoreText = "SCORE: " + score.ToString("D6");
            var scoreLocation = new Point((ScreenWidth - DrawnText.Width(scoreText)) / 2, 48);
            _score = new DrawnText { Location = scoreLocation, Text = scoreText };
            _entryController.OnUpdate = HandleInitialsUpdate;
        }

        private void HandleInitialsUpdate(string initials)
        {
            _initials.Text = initials.PadRight(InitialsBlank.Length, '_');
        }

        public void Update()
        {
            _entryController.Update();
        }

        public void Draw()
        {
            EnterInitials.Draw();
            _initials.Draw();
            _score.Draw();
            _instruction1.Draw();
            _instruction2.Draw();
        }
    }
}
