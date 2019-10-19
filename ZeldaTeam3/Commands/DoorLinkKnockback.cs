namespace Zelda.Commands
{
    internal class DoorLinkKnockback : ICommand
    {
        private readonly IPlayer _link;

        public DoorLinkKnockback(IPlayer link)
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
