namespace Zelda.Commands
{
    internal class EnemyRight : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyRight(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveRight();
            }
        }

        public override string ToString() => "Enemy: Face/move right";
    }
}