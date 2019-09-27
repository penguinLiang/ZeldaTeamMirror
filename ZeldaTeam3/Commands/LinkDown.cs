namespace Zelda.Commands
{
    internal class LinkDown : ICommand
    {
        private readonly IPlayer _link;

        public LinkDown(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FaceDown();
            _link.MoveDown();
        }

        public override string ToString() => "Link: Face/move down";
    }
}
