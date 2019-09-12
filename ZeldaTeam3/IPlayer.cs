namespace Zelda
{
    interface IPlayer : IMoveable, ISpawnable
    {
        void UsePrimaryItem();
        void UseSecondaryItem();
    }
}
