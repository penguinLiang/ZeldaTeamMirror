namespace Zelda.Commands
{
    class LinkLeft : ICommand
    {
        private readonly IPlayer _link;

        public LinkLeft(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.FaceLeft();
            _link.MoveLeft();
        }

        public override string ToString() => "Link faces left and then moves left";
    }
}
