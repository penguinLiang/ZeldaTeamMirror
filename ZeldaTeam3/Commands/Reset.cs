namespace Zelda.Commands
{
    class Reset : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public Reset(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.Resetting = true;
            _zeldaGame.Exit();
        }

        public override string ToString() => "Reset the game";
    }
}
