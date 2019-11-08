using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
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
            new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(GameWinMessage)));

        private const int ScreenHeight = 224;

        private static readonly Point RetryMessageLocation = new Point((512 - DrawnText.Width(GameWinMessage)) / 8, ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((512 - DrawnText.Width(GameWinMessage)) / 8, ScreenHeight - 120);
        private static readonly Point PressEnterLocation = new Point((512 - DrawnText.Width(PressEnter)) / 8, ScreenHeight - 80);

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText() { Location = GameWinMessageLocation, Text = GameWinMessage},
            new DrawnText() { Location = RetryMessageLocation, Text = RetryMessage},
            new DrawnText() { Location = QuitMessageLocation, Text = QuitMessage},
            new DrawnText() { Location = PressEnterLocation, Text = PressEnter}
        };

        private Vector2 _location;
        private string _selectedItem;

        private ISprite Cursor { get; set; }

        public GameWinMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = RetryMessage;
            _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
            Cursor = HUDSpriteFactory.Instance.CreateFullHeart();
        }

        public void Choose() {
            if (_selectedItem == RetryMessage)
            {
                var reset = new Commands.Reset(_agent);
                reset.Execute();
            }
            else
            {
                var quit = new Commands.Quit(_agent);
                quit.Execute();
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
            Cursor.Draw(_location);
            //Draw the heart selector next to the selected item
        }

        public void Update()
        {
          
        }

    }
}
