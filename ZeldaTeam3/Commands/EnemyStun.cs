namespace Zelda.Commands
{
    internal class EnemyStun : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyStun(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void Execute()
        {
            _enemy.Stun();
        }
    }
}
