namespace Zelda
{
    public interface IGameStateAgent : IDrawable
    {
        IDungeonManager DungeonManager { get; }
        IDrawable HUD { get; }
        IPlayer Player { get; }
        bool Quitting { get; }

        void Play();
        void Reset();
    }
}