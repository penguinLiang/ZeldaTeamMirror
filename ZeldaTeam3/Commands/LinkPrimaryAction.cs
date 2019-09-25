namespace Zelda.Commands
{
    class LinkPrimaryAction : ICommand
    {
        private readonly IPlayer _link;

        public LinkPrimaryAction(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link: Primary action";
    }
}
