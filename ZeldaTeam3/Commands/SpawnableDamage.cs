namespace Zelda.Commands
{
    internal class SpawnableDamage : ICommand
    {
        private readonly ISpawnable _spawnable;
        private readonly int _damage;

        public SpawnableDamage(ISpawnable spawnable, int damage)
        {
            _spawnable = spawnable;
            _damage = damage;
        }

        public void Execute()
        {
            _spawnable.TakeDamage(_damage);
        }
    }
}
