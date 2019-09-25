namespace Zelda.Commands
{
    class EnemyKill : ICommand
    {
        private readonly IEnemy[] _enemy;
        static int counter = 0;

        public EnemyKill(IEnemy[] Enemy)
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

            randomEnemy.Kill();

        }

        public override string ToString() => "Enemy: Kill";
    }
}