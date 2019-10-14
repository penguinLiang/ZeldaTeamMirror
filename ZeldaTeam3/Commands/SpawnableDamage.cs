namespace Zelda.Commands
{
    internal class SpawnableDamage : ICommand
    {
        private readonly ISpawnable _spawnable;

        public SpawnableDamage(ISpawnable spawnable)
        {
            _spawnable = spawnable;
        }

        public void Execute()
        {
            _spawnable.TakeDamage();
        }
    }
}
