using Zelda.Items;

namespace Zelda.Commands
{
    internal class LinkSecondaryAssign : ICommand
    {
        private readonly IPlayer _link;
        private readonly Secondary _item;

        public LinkSecondaryAssign(IPlayer link, Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(_item);
        }
    }
}
