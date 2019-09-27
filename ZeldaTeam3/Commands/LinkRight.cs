namespace Zelda.Commands
{
    internal class LinkRight : ICommand
    {
        private readonly IPlayer _link;

        public LinkRight(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FaceRight();
            _link.MoveRight();
        }

        public override string ToString() => "Link: Face/move right";
    }
}
