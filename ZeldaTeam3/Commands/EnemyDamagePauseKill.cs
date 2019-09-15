namespace Zelda.Commands
{
    class EnemyDamagePauseKill : ICommand
    {
        private readonly IEnemy _enemy;

        public EnemyDamagePauseKill(IEnemy Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            _enemy.TakeDamage();
            _enemy.Idle();
            _enemy.Kill();
        }

        public override string ToString() => "Enemy takes damage, pauses and then gets killed";
    }
}
