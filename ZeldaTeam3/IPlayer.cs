using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Items;
using Zelda.Player;

namespace Zelda
{
    public interface IPlayer : IHaltable, ISpawnable, IDrawable
    {
        int Health { get; }
        int MaxHealth { get; }

        List <IProjectile> Projectiles { get; }
        Inventory Inventory { get; }

        Point Location { get; }
        ICollideable BodyCollision { get; }
        ICollideable SwordCollision { get; }
        bool UsingPrimaryItem { get; }

        void Move(Direction direction);
        void UsePrimaryItem();
        void UseSecondaryItem();
        void AssignSecondaryItem(Secondary item);
        void Heal();
        void FullHeal();
        void AddHeart();
        void Teleport(Point location, Direction entranceDirection);

        bool Won { get; }
        void TouchTriforce();
    }
}
