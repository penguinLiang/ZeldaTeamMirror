namespace Zelda.Commands
{
    internal class Add1Rupee : ICommand
    {
        private readonly IPlayer _link;

        public Add1Rupee(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.Add1Rupee();
        }

        public override string ToString() => "Link: add one Rupee to inventory";
    }
}

