namespace Zelda.Commands
{
    internal class EnemySpawn : ICommand
    {
        private readonly IEnemy[] _enemy;

        public EnemySpawn(IEnemy[] enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            foreach (var enemy in _enemy)
            {
                enemy.Spawn();
            }
        }

        public override string ToString() => "Enemy: Spawn";
    }
}
