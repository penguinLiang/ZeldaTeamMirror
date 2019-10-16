namespace Zelda.Commands
{
    internal class LinkAddHeart : ICommand
    {
        private readonly IPlayer _link;

        public LinkAddHeart(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {   
            _link.AddHeart();
            _link.Heal();
        }

        public override string ToString() => "Link: Heal all hearts";
    }
}

