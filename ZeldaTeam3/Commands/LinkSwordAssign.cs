namespace Zelda.Commands
{
    internal class LinkSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkSwordAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.Sword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Assign/use sword";
    }
}
