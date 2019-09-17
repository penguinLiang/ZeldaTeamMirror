namespace Zelda.Commands
{
    class LinkMagicSwordAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkMagicSwordAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignPrimaryItem(Items.Primary.MagicalSword);
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link assigns magical sword to primary, then uses it";
    }
}
