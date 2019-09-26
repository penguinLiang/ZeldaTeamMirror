namespace Zelda
{
    public interface IEnemy : IMoveable, ISpawnable, IDrawable
    {
        void UseAttack();
    }
}
