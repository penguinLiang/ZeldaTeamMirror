using Zelda.Pause;

namespace Zelda.Commands
{
    internal class MenuSelectLeft : ICommand
    {
        private PauseMenu _pauseMenu;

        public MenuSelectLeft(PauseMenu pause)
        {
            _pauseMenu = pause;
        }

        public void Execute()
        {
            _pauseMenu.selectLeft();
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
