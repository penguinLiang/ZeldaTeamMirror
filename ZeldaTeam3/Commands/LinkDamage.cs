namespace Zelda.Commands
{
    class LinkDamage : ICommand
    {
        private readonly IPlayer _link;

        public LinkDamage(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.TakeDamage();
        }

        public override string ToString() => "Link takes damage";
    }
}
