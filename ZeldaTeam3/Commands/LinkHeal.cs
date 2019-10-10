namespace Zelda.Commands
{
    internal class LinkHeal : ICommand
    {
        private readonly IPlayer _link;

        public LinkHeal(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Heal();
        }

        public override string ToString() => "Link: Heal 1/2 heart";
    }
}

