using System;
using Microsoft.Xna.Framework;

namespace Zelda.HighScore
{
    public class Scoreboard : IDrawable
    {
        private const string WaitingMessage = "... FETCHING";
        private const int HUDOffsetY = 48;
        private const int ScreenHeight = 224;
        private const int ScreenWidth = 256;
        private const int displayedScores = 9;
        private const int FirstPlaceInitialsX = 48;
        private const int FirstPlaceTextX = 80;
        private const int FirstPlaceScoreX = 136;
        private const int FirstPlaceY = 32 - HUDOffsetY;
        private const int lineSpacing = 16;

        private static readonly IDrawable Background = new ScoreboardBackground();

        private readonly IDrawable[] _textDrawables = new DrawnText[displayedScores * 3];

        private bool scoresFetched;

        public Scoreboard()
        {
            _textDrawables[0] = new DrawnText { Text = WaitingMessage, Location = new Point(FirstPlaceInitialsX, FirstPlaceY) };
        }

        public void Update()
        {
            if (scoresFetched) return;

            try
            {
                PlayerScore[] scores = HighScoreClient.Scores();

                for (int i = 0; i < displayedScores && i < scores.Length; i++)
                {
                    _textDrawables[i] = new DrawnText { Text = scores[i].Initials,
                        Location = new Point(FirstPlaceInitialsX, FirstPlaceY + i * lineSpacing) };
                    _textDrawables[i + displayedScores] = new DrawnText { Text = "KILLED        MOBS",
                        Location = new Point(FirstPlaceTextX, FirstPlaceY + i * lineSpacing) };
                    _textDrawables[i + 2 * displayedScores] = new DrawnText { Text = scores[i].Score.ToString("D6"),
                        Location = new Point(FirstPlaceScoreX, FirstPlaceY + i * lineSpacing) };
                }
                scoresFetched = true;
            }
            catch (Exception)
            {
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
