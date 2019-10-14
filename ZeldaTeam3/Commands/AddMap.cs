namespace Zelda.Commands
{
    internal class AddMap : ICommand
    {
        private readonly IPlayer _link;

        public AddMap(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.AddMap();
        }

        public override string ToString() => "Link: add map to inventory";
    }
}

