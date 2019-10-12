namespace Zelda.Commands
{
    internal class MoveableHalt : ICommand
    {
        private readonly IHaltable _haltable;

        public MoveableHalt(IHaltable haltable)
        {
            _haltable = haltable;
        }

        public void Execute()
        {
            _haltable.Halt();
        }
    }
}
