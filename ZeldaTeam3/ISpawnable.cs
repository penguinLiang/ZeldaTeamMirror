namespace Zelda
{
    public interface ISpawnable
    {
        bool Alive { get; }

        void Spawn();
        void TakeDamage(int damage);
        void Stun();
    }
}
