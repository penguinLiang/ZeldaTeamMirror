namespace Zelda.Commands
{
    internal class LinkLeft : ICommand
    {
        private readonly IPlayer _link;

        public LinkLeft(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FaceLeft();
            _link.MoveLeft();
        }

        public override string ToString() => "Link: Face/move left";
    }
}
