namespace Zelda.Commands
{
    internal class LinkPrimaryAction : ICommand
    {
        private readonly IPlayer _link;

        public LinkPrimaryAction(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Primary action";
    }
}
