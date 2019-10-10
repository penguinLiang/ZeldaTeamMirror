namespace Zelda.Commands
{
    internal class LinkFullHeal : ICommand
    {
        private readonly IPlayer _link;

        public LinkFullHeal(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FullHeal();
        }

        public override string ToString() => "Link: Heal all hearts";
    }
}

