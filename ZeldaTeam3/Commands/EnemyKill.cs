namespace Zelda.Commands
{
    class EnemyKill : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyKill(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.Kill();
            }

        }

        public override string ToString() => "Enemy: Kill";
    }
}