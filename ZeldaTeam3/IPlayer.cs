namespace Zelda
{
    public interface IPlayer : IHaltable, ISpawnable, IDrawable
    {
        Player.Inventory Inventory { get; }

        void Move(Direction direction);
        void UsePrimaryItem();
        void UseSecondaryItem();
        void AssignSecondaryItem(Items.Secondary item);
        void Heal();
        void FullHeal();
    }
}
