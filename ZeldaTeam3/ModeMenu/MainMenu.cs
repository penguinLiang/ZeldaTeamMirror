using System;
using Microsoft.Xna.Framework;
using Zelda.HUD;

// ReSharper disable SwitchStatementMissingSomeCases

namespace Zelda.ModeMenu
{
    public class MainMenu : IDrawable, IMenu
    {
        private const int CursorX = 80;
        private const int TopCursorY = 120;
        private const int Offset = 16;
        private const int NumberOfOptions = 4;
        private const int DungeonPosition = 0;
        private const int LightsOutPosition = 1;
        private const int SurvivalPosition = 2;
        private const int QuitPosition = 3;

        private static readonly IDrawable Background = new MainMenuBackground();
        private readonly ZeldaGame _game;
        private readonly ISprite _cursor = HUDSpriteFactory.Instance.CreateFullHeart();

        private Point _cursorLocation = new Point(CursorX, TopCursorY);
        private int _optionNumber;

        public MainMenu(ZeldaGame game)
        {
            _game = game;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            Background.Draw();
            _cursor.Draw(_cursorLocation.ToVector2());
        }

        public void Choose()
        {
            switch (_optionNumber)
            {
                case DungeonPosition:
                    _game.SelectGameMode(false, false);
                    break;
                case LightsOutPosition:
                    _game.SelectGameMode(false, true);
                    break;
                case SurvivalPosition:
                    _game.SelectGameMode(true, false);
                    break;
                case QuitPosition:
                    _game.Exit();
                    return;
            }
        }

        public void SelectUp()
        {
            _optionNumber = Math.Max(0, _optionNumber - 1);
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

