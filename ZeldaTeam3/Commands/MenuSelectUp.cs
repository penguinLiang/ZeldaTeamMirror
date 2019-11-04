namespace Zelda.Commands
{
    internal class MenuSelectUp : ICommand
    {
        private readonly IMenu _menu;

        public MenuSelectUp(IMenu menu)
        {
            _menu = menu;
        }

        public void Execute()
        {
            _menu.SelectUp();
        }
    }
}
