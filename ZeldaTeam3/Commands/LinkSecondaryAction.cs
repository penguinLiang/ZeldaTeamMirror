namespace Zelda.Commands
{
    class LinkSecondaryAction : ICommand
    {
        private readonly IPlayer _link;

        public LinkSecondaryAction(IPlayer Link)
        {
            _link = Link;
        }

        public void Execute()
        {
            _link.UsePrimaryItem();
        }

        public override string ToString() => "Link uses Secondary action";
    }
}
