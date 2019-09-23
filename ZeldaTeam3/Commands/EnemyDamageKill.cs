namespace Zelda.Commands
{
    class EnemyDamagePauseKill : ICommand
    {
        private readonly IEnemy[] _enemy;
        static int counter = 0;

        public EnemyDamagePauseKill(IEnemy[] Enemy)
        {
            _enemy = Enemy;
        }

        public void Execute()
        {
            IEnemy randomEnemy = _enemy[counter];
            counter++;
            if(counter >= _enemy.Length)
            {
                counter = 0;
            }

            randomEnemy.TakeDamage();
            randomEnemy.Kill();

        }

        public override string ToString() => "Enemy: Damage, Kill";
    }
}