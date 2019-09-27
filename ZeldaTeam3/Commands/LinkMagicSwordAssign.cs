namespace Zelda.Commands
{
    internal class LinkMagicSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkMagicSwordAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.MagicalSword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Assign/use magic sword";
    }
}
