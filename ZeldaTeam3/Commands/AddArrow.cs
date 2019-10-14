namespace Zelda.Commands
{
    internal class AddArrow : ICommand
    {
        private readonly IPlayer _link;

        public AddArrow(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.AddArrow();
        }

        public override string ToString() => "Link: add arrow to inventory";
    }
}

