namespace Zelda.Commands
{
    class LinkSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkSwordAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.Sword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link assigns sword to primary, then uses it";
    }
}
