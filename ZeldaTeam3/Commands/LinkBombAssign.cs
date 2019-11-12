using Zelda.Items;

namespace Zelda.Commands
{
    internal class LinkBombAssign : ICommand
    {
        private readonly IPlayer _link;

        public LinkBombAssign(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(Secondary.Bomb);
            _link.UseSecondaryItem();
        }

        public override string ToString() => "Link: Assign/use bomb";
    }
}
