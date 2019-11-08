namespace Zelda.Commands
{
    internal class MenuSelectChoice : ICommand
    {
        private readonly IMenu _menu;

        public MenuSelectChoice(IMenu menu)
        {
            _menu = menu;
        }

        public void Execute()
        {
            _menu.Choose();
        }
    }
}
