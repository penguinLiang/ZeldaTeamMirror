namespace Zelda.Commands
{
    internal class EnemyUseAttack : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyUseAttack(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.UseAttack();
            }
        }

        public override string ToString() => "Enemy: Use Attack";
    }
}