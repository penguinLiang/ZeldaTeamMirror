using Zelda.Dungeon;

namespace Zelda
{
    public interface IGameStateAgent : IDrawable
    {
        DungeonManager DungeonManager { get; }
        IDrawable HUD { get; }
        IPlayer Player { get; }
        bool Quitting { get; }

        void Play();
        void Reset();
    }
}