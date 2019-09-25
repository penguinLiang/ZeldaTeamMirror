namespace Zelda.Commands
{
    class EnemyDown : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyDown(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.MoveDown();
            }
        }

        public override string ToString() => "Enemy: Face/move down";
    }
}
