using System;
using Microsoft.Xna.Framework;

namespace Zelda.HighScore
{
    public class Scoreboard : IDrawable
    {
        private const string WaitingMessage = "FETCHING SCORES ...";
        private const string TimeoutMessage = "FAILED TO GET SCORES";
        private const int DisplayedScores = 9;
        private const int MaxInitials = 3;
        private const int FirstPlaceX = 48;
        private const int FirstPlaceY = 32 - HUD.HUDSpriteFactory.ScreenHeight;
        private const int LineSpacing = 16;
        private const int MaxTries = 3;

        private static readonly IDrawable Background = new ScoreboardBackground();

        private readonly IDrawable[] _textDrawables = new DrawnText[DisplayedScores];

        private bool _scoresFetched;
        private int _failedTries;

        public Scoreboard()
        {
            _textDrawables[0] = new DrawnText { Text = WaitingMessage, Location = new Point(FirstPlaceX, FirstPlaceY) };
        }

        public void Update()
        {
            if (_scoresFetched || _failedTries >= MaxTries) return;

            try
            {
                PlayerScore[] scores = HighScoreClient.Scores();

                for (int i = 0; i < DisplayedScores && i < scores.Length; i++)
                {
                    String initials = scores[i].Initials;
                    while (initials.Length < MaxInitials)
                    {
                        initials += " ";
                    }

                    _textDrawables[i] = new DrawnText {
                        Text = initials + " KILLED " + scores[i].Score.ToString("D6") + " MOBS",
                        Location = new Point(FirstPlaceX, FirstPlaceY + i * LineSpacing)
                    };
                }
                _scoresFetched = true;
            }
            catch (Exception)
            {
                if (++_failedTries == MaxTries)
                    _textDrawables[0] = new DrawnText { Text = TimeoutMessage, Location = new Point(FirstPlaceX, FirstPlaceY) };
            }
        }

        public void Draw()
        {
            Background.Draw();
            foreach (var text in _textDrawables)
            {
                text?.Draw();
            }
        }
    }
}
