using Zelda.Pause;

namespace Zelda.Commands
{
    internal class MenuSelectRight : ICommand
    {
        private PauseMenu _pauseMenu;

        public MenuSelectRight(PauseMenu pause)
        {
            _pauseMenu = pause;
        }

        public void Execute()
        {
            _pauseMenu.selectRight();
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
