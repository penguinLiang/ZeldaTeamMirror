namespace Zelda.Commands
{
    class LinkBoomerangAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBoomerangAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(Items.Secondary.Boomerang);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use boomerang";
    }
}
