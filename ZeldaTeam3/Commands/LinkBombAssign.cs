namespace Zelda.Commands
{
    class LinkBombAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBombAssign(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(Items.Secondary.Bomb);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use bomb";
    }
}
