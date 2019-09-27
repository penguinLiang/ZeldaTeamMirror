namespace Zelda.Commands
{
    internal class LinkSecondaryAction : ICommand
    {
        private readonly IPlayer _link;

        public LinkSecondaryAction(IPlayer link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.UseSecondaryItem();
        }
        public override string ToString() => "Link: Secondary action";
    }
}
