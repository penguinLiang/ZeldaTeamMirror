namespace Zelda.Commands
{
    class LinkRight : ICommand
    {
        private readonly IPlayer _link;

        public LinkRight(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.FaceRight();
            _link.MoveRight();
        }

        public override string ToString() => "Link faces right and then moves right";
    }
}
