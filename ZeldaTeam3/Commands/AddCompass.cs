namespace Zelda.Commands
{
    internal class AddCompass : ICommand
    {
        private readonly IPlayer _link;

        public AddCompass(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Inventory.AddCompass();
        }

        public override string ToString() => "Link: add compass to inventory";
    }
}

