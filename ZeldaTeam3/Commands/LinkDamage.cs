namespace Zelda.Commands
{
    internal class LinkDamage : ICommand
    {
        private readonly IPlayer _link;

        public LinkDamage(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.TakeDamage();
        }

        public override string ToString() => "Link: Take damage";
    }
}
