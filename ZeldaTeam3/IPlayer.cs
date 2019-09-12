namespace Zelda
{
    interface IPlayer : IMoveable, ISpawnable
    {
        void UsePrimaryItem();
        void UseSecondaryItem();
        void AssignPrimaryItem(Items.Primary item);
        void AssignSecondaryItem(Items.Secondary item);
    }
}
