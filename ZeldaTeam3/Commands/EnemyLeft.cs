namespace Zelda.Commands
{
    class EnemyLeft : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyLeft(IEnemy Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            _enemy.FaceLeft();
            _enemy.MoveLeft();
        }

        public override string ToString() => "Enemy faces left and then moves left";
    }
}
