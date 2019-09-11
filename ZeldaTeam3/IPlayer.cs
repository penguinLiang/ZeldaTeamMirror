namespace Zelda
{
    interface IPlayer : IMoveable, ICharacter
    {
        void UsePrimaryItem();
        void UseSecondaryItem();
    }
}
