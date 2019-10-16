namespace Zelda.Commands
{
    internal class AddKey : ICommand
    {
        private readonly IPlayer _link;

        public AddKey(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.AddKey();
        }

        public override string ToString() => "Link: add key to inventory";
    }
}

