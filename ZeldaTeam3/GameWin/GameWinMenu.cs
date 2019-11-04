using System;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.Commands;
using Zelda.GameState;

namespace Zelda.GameWin
{
  public class GameWinMenu : IMenu, IDrawable
    {
        private readonly GameStateAgent _agent;
        private readonly Vector2 _location;
        private Point _cursorPosition;
        private string _selectedItem;

        


        public GameWinMenu(GameStateAgent agent, Point location)
        {
            _agent = agent;
            _location = location.ToVector2();
          
        }


        public void Choose() {
            //Depending on what is selected, activate that
        }

    public void SelectUp()
        {

        }
       public void SelectDown()
        {

        }
       public void SelectLeft()
        {
            //no op, can only select up or down in win/gameover menu
        }
       public void SelectRight()
        {
            //no op, can only select up or down in win/gameover menu
        }

        public void Draw()
        {

        }

        public void Update()
        {

        }

    }
}
