namespace Zelda.Commands
{
    class Quit : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public Quit(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.Exit();
        }

        public override string ToString() => "Quit the game";
    }
}
