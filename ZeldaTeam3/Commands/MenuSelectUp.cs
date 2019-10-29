using Zelda.Pause;

namespace Zelda.Commands
{
    internal class MenuSelectUp : ICommand
    {
        private PauseMenu _pauseMenu;

        public MenuSelectUp(PauseMenu pause)
        {
            _pauseMenu = pause;
        }

        public void Execute()
        {
            _pauseMenu.selectUp();
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
