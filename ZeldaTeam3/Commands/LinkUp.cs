namespace Zelda.Commands
{
    internal class LinkUp : ICommand
    {
        private readonly IPlayer _link;

        public LinkUp(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FaceUp();
            _link.MoveUp();
        }

        public override string ToString() => "Link: Face/move up";
    }
}
