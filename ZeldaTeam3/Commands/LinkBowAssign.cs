using Zelda.Items;

namespace Zelda.Commands
{
    internal class LinkBowAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBowAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(Secondary.Bow);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use bow";
    }
}
