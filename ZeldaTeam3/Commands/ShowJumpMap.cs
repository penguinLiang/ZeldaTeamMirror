namespace Zelda.Commands
{
    internal class ShowJumpMap : ICommand
    {
        private readonly ZeldaGame _game;

        public ShowJumpMap(ZeldaGame game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.JumpMap.Visible = true;
        }
    }
}
