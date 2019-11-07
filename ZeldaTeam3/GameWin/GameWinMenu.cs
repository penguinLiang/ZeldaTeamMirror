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

        public GameWinMenu(GameStateAgent agent)
        {
            _agent = agent;          
        }

        public void Choose() {
            var reset = new Commands.Reset(_agent);
            reset.Execute();
        }

    public void SelectUp()
        {
            //no op, only option is to select 'play again' or press 'q' to quit
        }
       public void SelectDown()
        {
            //no op, only option is to select 'play again' or press 'q' to quit

        }
        public void SelectLeft()
        {
            //no op, only option is to select 'play again' or press 'q' to quit
        }
        public void SelectRight()
        {
            //no op, only option is to select 'play again' or press 'q' to quit
        }

        public void Draw()
        {

        }

        public void Update()
        {

        }

    }
}
