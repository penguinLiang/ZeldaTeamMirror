using Zelda.Items;

namespace Zelda.Commands
{
    internal class LinkSecondaryAddDebug : ICommand
    {
        private readonly IPlayer _link;
        private readonly Secondary _item;

        public LinkSecondaryAddDebug(IPlayer link, Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.Inventory.AddSecondaryItem(Secondary.Arrow);
            _link.Inventory.AddSecondaryItem(_item);
            _link.AssignSecondaryItem(_item);
            _link.UseSecondaryItem();
        }
    }
}
