namespace Zelda
{
    class CommandSetMovingAnimated : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public CommandSetMovingAnimated(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.CurrentSprite = _zeldaGame.WalkingLink;
        }
    }
}
