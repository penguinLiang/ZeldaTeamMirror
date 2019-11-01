namespace Zelda.Commands
{
    internal class MenuSelectDown : ICommand
    {
        private readonly IMenu _menu;

        public MenuSelectDown(IMenu menu)
        {
            _menu = menu;
        }

        public void Execute()
        {
            _menu.SelectDown();
        }
    }
}
