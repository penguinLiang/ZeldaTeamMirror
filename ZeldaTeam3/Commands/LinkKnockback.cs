namespace Zelda.Commands
{
    internal class LinkKnockback : ICommand
    {
        private readonly IPlayer _link;

        public LinkKnockback(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Halt();
            _link.Knockback();
        }
        public override string ToString() => "Link: Knockback";
    }
}
