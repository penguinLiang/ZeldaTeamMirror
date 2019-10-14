namespace Zelda.Commands
{
    internal class Add5Rupee : ICommand
    {
        private readonly IPlayer _link;

        public Add5Rupee(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.Add5Rupee();
        }

        public override string ToString() => "Link: add 5 Rupees to inventory";
    }
}

