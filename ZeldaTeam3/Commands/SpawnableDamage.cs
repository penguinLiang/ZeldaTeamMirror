namespace Zelda.Commands
{
    internal class SpawnableDamage : ICommand
    {
        private readonly ISpawnable _spawnable;
        private int _damage;

        public SpawnableDamage(ISpawnable spawnable, int damage)
        {
            _spawnable = spawnable;
            _damage = damage;
        }

        public void Execute()
        {
            for (int i = 0; i < _damage; i++)
            {
                _spawnable.TakeDamage();
            }
        }
    }
}
