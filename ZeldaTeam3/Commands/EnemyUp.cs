namespace Zelda.Commands
{
    class EnemyUp : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyUp(IEnemy Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            _enemy.FaceUp();
            _enemy.MoveUp();
        }

        public override string ToString() => "Enemy faces up and then moves up";
    }
}
