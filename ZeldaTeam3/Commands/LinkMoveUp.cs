namespace Zelda.Commands
{
    internal class LinkMoveUp : ICommand
    {
        private readonly IPlayer _link;

        public LinkMoveUp(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Move(Direction.Up);
        }

        public override string ToString() => "Link: Move up";
    }
}
