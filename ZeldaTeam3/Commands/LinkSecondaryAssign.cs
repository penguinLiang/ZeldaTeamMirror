namespace Zelda.Commands
{
    internal class LinkSecondaryAssign : ICommand
    {
        private readonly IPlayer _link;
        private Items.Secondary _item;

        public LinkSecondaryAssign(IPlayer link, Items.Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(_item);
        }

        public override string ToString() => "Link: Assign secondary item";
    }
}
