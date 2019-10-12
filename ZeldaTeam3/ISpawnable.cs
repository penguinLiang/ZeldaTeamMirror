namespace Zelda
{
    public interface ISpawnable
    {
        bool Alive { get; }

        void Spawn();
        void TakeDamage();
        void Stun();
    }
}
