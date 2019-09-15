namespace Zelda.Commands
{
    class LinkBowAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBowAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(Items.Secondary.Bow);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link assigns bow to secondary slot, then uses it";
    }
}
