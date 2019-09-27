namespace Zelda.Commands
{
    internal class EnemyUp : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemyUp(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.MoveUp();
            }
        }

        public override string ToString() => "Enemy: Face/move up";
    }
}
