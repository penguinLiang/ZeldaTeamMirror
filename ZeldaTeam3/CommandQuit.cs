namespace Zelda
{
    class CommandQuit : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public CommandQuit(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.Exit();
        }
    }
}
