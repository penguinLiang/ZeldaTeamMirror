namespace Zelda.Commands
{
    internal class LinkSecondaryAssign : ICommand
    {
        private readonly IPlayer _link;
        private readonly Items.Secondary _item;

        public LinkSecondaryAssign(IPlayer link, Items.Secondary item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            _link.AssignSecondaryItem(_item);
        }
    }
}
