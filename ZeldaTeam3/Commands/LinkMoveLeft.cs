namespace Zelda.Commands
{
    internal class LinkMoveLeft : ICommand
    {
        private readonly IPlayer _link;

        public LinkMoveLeft(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Move(Direction.Left);
        }

        public override string ToString() => "Link: Face/move left";
    }
}
