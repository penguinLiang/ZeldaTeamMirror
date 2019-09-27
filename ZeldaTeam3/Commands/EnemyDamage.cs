namespace Zelda.Commands
{
    internal class EnemyDamage : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyDamage(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.TakeDamage();
            }
        }

        public override string ToString() => "Enemy: Take damage";
    }
}
