namespace Zelda.Commands
{
    internal class MenuSelectLeft : ICommand
    {
        private readonly IMenu _menu;

        public MenuSelectLeft(IMenu menu)
        {
            _menu = menu;
        }

        public void Execute()
        {
            _menu.SelectLeft();
        }
    }
}
