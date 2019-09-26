namespace Zelda.Commands
{
    class EnemyLeft : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyLeft(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.MoveLeft();
            }
        }

        public override string ToString() => "Enemy: Face/move left";
    }
}