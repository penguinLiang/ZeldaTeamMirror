namespace Zelda.Commands
{
    class EnemyDown : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyDown(IEnemy Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            _enemy.FaceDown();
            _enemy.MoveDown();
        }

        public override string ToString() => "Enemy faces down and then moves down";
    }
}
