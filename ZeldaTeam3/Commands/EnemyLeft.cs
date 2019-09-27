namespace Zelda.Commands
{
    internal class EnemyLeft : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyLeft(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveLeft();
            }
        }

        public override string ToString() => "Enemy: Face/move left";
    }
}