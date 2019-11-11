namespace Zelda.Commands
{
    internal class LinkBoomerangAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBoomerangAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.AddSecondaryItem(Items.Secondary.Boomerang);
            _link.AssignSecondaryItem(Items.Secondary.Boomerang);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use boomerang";
    }
}
