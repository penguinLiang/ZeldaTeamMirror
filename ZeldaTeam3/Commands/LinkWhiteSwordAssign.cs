namespace Zelda.Commands
{
    internal class LinkWhiteSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkWhiteSwordAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.WhiteSword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Assign/use white sword";
    }
}
