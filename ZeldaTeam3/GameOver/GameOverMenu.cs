using System;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.Commands;
using Zelda.GameState;
using Zelda.HUD;

namespace Zelda.GameOver
{
    internal class GameOverMenu : IDrawable, IMenu
    {
        private const string GameOverMessage = "GAME OVER";
        private static readonly int ScreenHeight = 224;
        private static readonly Point GameOverMessageLocation =
            new Point((HUDSpriteFactory.ScreenWidth - DrawnText.Width(GameOverMessage)) / 2, 0);

        private const string RetryMessage = "Retry";
        private const string QuitMessage = "Quit";
        private static readonly Point RetryMessageLocation = new Point((512 - DrawnText.Width(GameOverMessage)) / 8 , ScreenHeight - 140);
        private static readonly Point QuitMessageLocation = new Point((512- DrawnText.Width(GameOverMessage)) / 8, ScreenHeight - 120);
        
        private readonly IDrawable[] _textDrawables =
        {
            new DrawnText() { Location = GameOverMessageLocation, Text = GameOverMessage },
            new DrawnText() {Location = RetryMessageLocation, Text = RetryMessage},
            new DrawnText() {Location = QuitMessageLocation, Text = QuitMessage}
        };

        private readonly GameStateAgent _agent;
        private Vector2 _location;
        private string _selectedItem;
        private ISprite _cursor = ItemSpriteFactory.Instance.CreateDroppedHeart();


        public GameOverMenu(GameStateAgent agent)
        {
            _agent = agent;
            _selectedItem = RetryMessage;
            _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
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
            if(_selectedItem == RetryMessage)
            {
                _cursor.Draw(_location);
            //draw heart selector next to retry
            }
            else
            {
                //draw heart selector next to quit
                _cursor.Draw(_location);
            }

        }

        public void Choose()
        {
           if(_selectedItem == RetryMessage)
            {
                //reset
            }
           else
            {
                //quit
            }
        }

        public void SelectUp()
        {
            if(_selectedItem == RetryMessage)
            {
               // Already at the top of the menu, no op
            }
            else
            {
                _selectedItem = QuitMessage;
                _location = new Vector2(QuitMessageLocation.X - 16, QuitMessageLocation.Y);
                _cursor.Draw(_location);
            }
        }

        public void SelectDown()
        {
            if (_selectedItem == QuitMessage)
            {
                // Already at the bottom of the menu, no op
            }
            else
            {
                _selectedItem = RetryMessage;
                _location = new Vector2(RetryMessageLocation.X - 16, RetryMessageLocation.Y);
                _cursor.Draw(_location);
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
