namespace Zelda.Commands
{
    class EnemyRight : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyRight(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            foreach (IEnemy enemy in _enemy)
            {
                enemy.MoveRight();
            }
        }

        public override string ToString() => "Enemy: Face/move right";
    }
}