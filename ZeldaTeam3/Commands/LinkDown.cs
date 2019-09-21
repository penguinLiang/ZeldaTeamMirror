namespace Zelda.Commands
{
    class LinkDown : ICommand
    {
        private readonly IPlayer _link;

        public LinkDown(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.FaceDown();
            _link.MoveDown();
        }

        public override string ToString() => "Link faces/move down";
    }
}
