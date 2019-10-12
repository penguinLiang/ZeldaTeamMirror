namespace Zelda.Commands
{
    internal class NoOp : ICommand
    {
        public static NoOp Instance { get; } = new NoOp();

        public void Execute()
        {
            // NO-OP: Intentionally does nothing to avoid null checks
        }
    }
}
