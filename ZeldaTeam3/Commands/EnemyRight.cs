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
                enemy.FaceRight();
                enemy.MoveRight();
            }
        }

        public override string ToString() => "Enemy faces right and then moves right";
    }
}