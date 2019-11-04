using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Zelda
{
    public interface IPlayer : IHaltable, ISpawnable, IDrawable
    {
        int Health { get; }
        int MaxHealth { get; }

        List <IProjectile> Projectiles { get; set; }
        //factor out to a master class?
        Player.Inventory Inventory { get; }

        ICollideable BodyCollision { get; }
        ICollideable SwordCollision { get; }
        bool UsingPrimaryItem { get; }

        bool UsingSecondaryItem { get; }

        Direction Direction { get; }
        Point Location { get; }

        void Move(Direction direction);
        void UsePrimaryItem();
        void UseSecondaryItem();
        void AssignSecondaryItem(Items.Secondary item);
        void Heal();
        void FullHeal();
        void AddHeart();
        void Teleport(Point location, Direction entranceDirection);
    }
}
