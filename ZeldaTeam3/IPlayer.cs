namespace Zelda
{
    public interface IPlayer : IHaltable, ISpawnable, IDrawable
    {
        Player.Inventory Inventory { get; }

        ICollideable BodyCollision { get; }
        ICollideable SwordCollision { get; }
        bool UsingPrimaryItem { get; }

        void Move(Direction direction);
        void UsePrimaryItem();
        void UseSecondaryItem();
        void AssignSecondaryItem(Items.Secondary item);
        void Heal();
        void FullHeal();
        void AddHeart();
        void TeleportToEntrance(Direction entranceDirection);
    }
}
