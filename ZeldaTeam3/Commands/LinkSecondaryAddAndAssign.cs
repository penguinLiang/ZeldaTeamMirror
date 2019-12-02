using Zelda.Items;

namespace Zelda.Commands
{
    internal class LinkSecondaryAddAndAssign : ICommand
    {
        private readonly IPlayer _link;
        private readonly Secondary _item;

        public LinkSecondaryAddAndAssign(IPlayer link, Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.Inventory.AddSecondaryItem(_item);
            _link.AssignSecondaryItem(_item);
        }
    }
}
