namespace Zelda
{
    class CommandSetMovingStatic : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public CommandSetMovingStatic(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.CurrentSprite = _zeldaGame.JumpingLink;
        }
    }
}
