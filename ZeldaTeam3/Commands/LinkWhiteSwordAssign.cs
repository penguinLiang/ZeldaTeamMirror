namespace Zelda.Commands
{
    class LinkWhiteSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkWhiteSwordAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.WhiteSword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Assign/use white sword";
    }
}
