namespace Zelda.Commands
{
    internal class MenuSelectRight : ICommand
    {
        private readonly IMenu _menu;

        public MenuSelectRight(IMenu menu)
        {
            _menu = menu;
        }

        public void Execute()
        {
            _menu.SelectRight();
        }
    }
}
