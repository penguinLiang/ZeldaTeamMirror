namespace Zelda
{
    class CommandSetFixedStatic : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public CommandSetFixedStatic(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.CurrentSprite = _zeldaGame.StandingLink;
        }
    }
}
