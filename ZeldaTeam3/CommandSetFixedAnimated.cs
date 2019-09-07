namespace Zelda
{
    class CommandSetFixedAnimated : ICommand
    {
        private readonly ZeldaGame _zeldaGame;

        public CommandSetFixedAnimated(ZeldaGame zeldaGame)
        {
            _zeldaGame = zeldaGame;
        }

        public void Execute()
        {
            _zeldaGame.CurrentSprite = _zeldaGame.StabbingLink;
        }
    }
}
