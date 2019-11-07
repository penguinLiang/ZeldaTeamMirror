using System;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.Commands;
using Zelda.GameState;
using Zelda.HUD;

namespace Zelda.GameOver
{
    public class GameOverMenu : IDrawable, IMenu
    {
        private const string GameOverMessage = "GAME OVER";
        private static readonly int ScreenHeight = 224;
        private static readonly Point GameOverMessageLocation =
            new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(GameOverMessage)) / 2, 0);

        private const string ContinueMessage = "Continue";
        private const string RetryMessage = "Retry";
        private const string QuitMessage = "Quit";
        private const string PressEnter = "Press Enter to Select";

        private static readonly Point ContinueMessageLocation = new Point((512 - DrawnText.Width(GameOverMessage)) / 8 , ScreenHeight - 160);
        private static readonly Point RetryMessageLocation = new Point((512 - DrawnText.Width(GameOverMessage)) / 8 , ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((512- DrawnText.Width(GameOverMessage)) / 8, ScreenHeight - 120);
        private static readonly Point PressEnterLocation = new Point((512 - DrawnText.Width(PressEnter)) / 8, ScreenHeight - 80);

        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText() { Location = GameOverMessageLocation, Text = GameOverMessage },
            new DrawnText() {Location = ContinueMessageLocation, Text = ContinueMessage},
            new DrawnText() {Location = RetryMessageLocation, Text = RetryMessage},
            new DrawnText(){Location = QuitMessageLocation, Text = QuitMessage},
            new DrawnText() {Location = PressEnterLocation, Text = PressEnter}
        };

        private readonly GameStateAgent _agent;
        private Vector2 _location;
        private string _selectedItem;
       private string OldSelect { get; set; }

        private ISprite Cursor { get; set; }

        public GameOverMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = ContinueMessage;
            _location = new Vector2(ContinueMessageLocation.X - 16, ContinueMessageLocation.Y);
            Cursor = HUDSpriteFactory.Instance.CreateFullHeart();
            OldSelect = ContinueMessage;
            //need space for the heart selector icon
        }

        public void Update()
        {
           
        }

        public void Draw()
        {
            foreach (var textDrawable in _textDrawables)
            {
                textDrawable.Draw();
            }
                Cursor.Draw(_location);
            //draw heart selector next to current selected option

        }

        public void Choose()
        {

            if (_selectedItem == ContinueMessage)
            {
                //continue
                _agent.Continue();
            }
            else if(_selectedItem == RetryMessage)
            {
                //reset
                var reset = new Commands.Reset(_agent);
                reset.Execute();
            }
           else
            {
                //quit
                var quit = new Commands.Quit(_agent);
                quit.Execute();
            }
        }

        public void SelectUp()
        {
           OldSelect = _selectedItem;
            if (OldSelect == QuitMessage)
            {
                _selectedItem = RetryMessage;
                _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
            }
            else if (OldSelect == RetryMessage)
            {
                _selectedItem = ContinueMessage;
                _location = new Vector2(ContinueMessageLocation.X - 16, ContinueMessageLocation.Y);
            }
           
        }

        public void SelectDown()
        {
            string oldSelect = _selectedItem;
            if (oldSelect == RetryMessage)
            {
              //  OldSelect = QuitMessage;
                _selectedItem = QuitMessage;
                _location = new Vector2(QuitMessageLocation.X - 16, QuitMessageLocation.Y);
            }
            else if (oldSelect == ContinueMessage)
            {
             //   OldSelect = RetryMessage;
                _selectedItem = RetryMessage;
                _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
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
