namespace Zelda
{
    interface IEnemy : IMoveable, ISpawnable
    {
        void UseAttack();
    }
}
