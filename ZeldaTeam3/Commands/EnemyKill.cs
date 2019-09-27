namespace Zelda.Commands
{
    internal class EnemyKill : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyKill(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.Kill();
            }
        }

        public override string ToString() => "Enemy: Kill";
    }
}