using Zelda.Items;

namespace Zelda.Commands
{
    internal class AddSecondaryItem : ICommand
    {
        private readonly IPlayer _link;
        private readonly Secondary _item;

        public AddSecondaryItem(IPlayer link, Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.Inventory.AddSecondaryItem(_item);
            if (_link.Inventory.SecondaryItem == Secondary.None)
                _link.AssignSecondaryItem(_item);
        }

        public override string ToString() => "Link: Add "+ _item +" to inventory";
    }
}

