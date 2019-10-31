using Zelda.Pause;

namespace Zelda.Commands
{
    internal class MenuUnpause : ICommand
    {
        private PauseMenu _pauseMenu;

        public MenuUnpause(PauseMenu pause)
        {
            _pauseMenu = pause;
        }

        public void Execute()
        {
            _pauseMenu.unpause();
        }

        public override string ToString() => "Pause/Unpause menu";
    }
}
