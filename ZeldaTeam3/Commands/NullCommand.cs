namespace Zelda.Commands
{
    internal class NullCommand : ICommand
    {
        private readonly IPlayer _link;

        public NullCommand()
        {

        }

        public void Execute()
        {
            
        }

        public override string ToString() => "Filler Command. Nothing happens.";
    }
}
