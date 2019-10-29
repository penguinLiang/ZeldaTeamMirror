using Zelda.Pause;

namespace Zelda.Commands
{
    internal class MenuSelectDown : ICommand
    {
        private PauseMenu _pauseMenu;

        public MenuSelectDown(PauseMenu pause)
        {
            _pauseMenu = pause;
        }

        public void Execute()
        {
            _pauseMenu.selectDown();
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
