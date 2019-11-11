using Zelda.Items;

namespace Zelda.Commands
{
    internal class UpgradeSword : ICommand
    {
        private readonly IPlayer _link;
        private readonly Primary _item;

        public UpgradeSword(IPlayer link, Primary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.Inventory.UpgradeSword(_item);
        }

        public override string ToString() => "Link: Upgrade sword";
    }
}

