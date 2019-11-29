using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.GameState;
using Zelda.Items;

namespace Zelda.MainMenu
{
    public partial class MainMenu : IDrawable, IMenu
    {
        private const int CursorX = 80;
        private const int TopCursorY = 120;
        private const int Offset = 16;
        private const int NumberOfOptions = 3;

        private static readonly IDrawable _background = new MainMenuBackground();
        private readonly ISprite _cursor = HUD.HUDSpriteFactory.Instance.CreateFullHeart();
        private readonly Point _topCursorPosition = new Point(80, 120);

        private Point _cursorLocation = new Point(CursorX, TopCursorY);
        private int _optionNumber;

        public MainMenu()
        {

        }

        public void Update()
        {
        }

        public void Draw()
        {
            _background.Draw();
            _cursor.Draw(_cursorLocation.ToVector2());
        }

        public void Choose()
        {
            // NO-OP: Pause menu selects instantly
        }

        public void SelectUp()
        {
            _optionNumber = Math.Min(0, _optionNumber - 1);
            _cursorLocation.Y = TopCursorY + Offset * _optionNumber;
        }

        public void SelectDown()
        {
            _optionNumber = Math.Min(NumberOfOptions - 1, _optionNumber + 1);
            _cursorLocation.Y = TopCursorY + Offset * _optionNumber;
        }

        public void SelectLeft()
        {
            // NO-OP
        }

        public void SelectRight()
        {
            // NO-OP
        }
    }
}

