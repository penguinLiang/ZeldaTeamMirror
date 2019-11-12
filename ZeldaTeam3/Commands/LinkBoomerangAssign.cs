using Zelda.Items;

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
            _link.Inventory.AddSecondaryItem(Secondary.Boomerang);
            _link.AssignSecondaryItem(Secondary.Boomerang);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use boomerang";
    }
}
