namespace Zelda.Commands
{
    internal class LinkMoveRight : ICommand
    {
        private readonly IPlayer _link;

        public LinkMoveRight(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Move(Direction.Right);
        }

        public override string ToString() => "Link: Move right";
    }
}
