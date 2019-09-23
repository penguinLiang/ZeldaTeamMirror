namespace Zelda.Commands
{
    class LinkUp : ICommand
    {
        private readonly IPlayer _link;

        public LinkUp(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.FaceUp();
            _link.MoveUp();
        }

        public override string ToString() => "Link: Face/move up";
    }
}
