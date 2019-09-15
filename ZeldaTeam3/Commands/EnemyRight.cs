namespace Zelda.Commands
{
    class EnemyRight : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyRight(IEnemy Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            _enemy.FaceRight();
            _enemy.MoveRight();
        }

        public override string ToString() => "Enemy faces right and then moves right";
    }
}
