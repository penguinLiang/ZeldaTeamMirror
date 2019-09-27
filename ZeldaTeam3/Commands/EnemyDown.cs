namespace Zelda.Commands
{
    internal class EnemyDown : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyDown(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveDown();
            }
        }

        public override string ToString() => "Enemy: Face/move down";
    }
}
