namespace Zelda
{
    public interface IEnemy : IMoveable, ISpawnable
    {
        void UseAttack();
    }
}
