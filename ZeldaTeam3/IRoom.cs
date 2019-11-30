using System.Collections.Generic;
using Zelda.Blocks;

namespace Zelda
{
    public interface IRoom
    {
        void TransitionReset();
        List<IEnemy> Enemies { get; }
        bool SomeEnemiesAlive { get; }
        List<ICollideable> Collidables { get; }
        List<IDrawable> Drawables { get; }
        List<IItem> Items { get; }
        List<ITransitionResetable> TransitionResetables { get; }
        Dictionary<Direction, DoorBase> Doors { get; }
    }
}