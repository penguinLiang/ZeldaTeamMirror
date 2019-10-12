namespace Zelda.Commands
{
    internal class LinkMoveDown : ICommand
    {
        private readonly IPlayer _link;

        public LinkMoveDown(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Move(Direction.Down);
        }

        public override string ToString() => "Link: Move down";
    }
}
