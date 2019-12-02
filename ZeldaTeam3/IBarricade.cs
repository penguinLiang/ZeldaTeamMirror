namespace Zelda
{
   public interface IBarricade: ICollideable, IDrawable
    {
         bool Unlocked { get; set; }
         void Unlock();
         void Reset();
    }
}
