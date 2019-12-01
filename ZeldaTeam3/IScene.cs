namespace Zelda
{
    public interface IScene : IDrawable
    {
        void DestroyProjectiles();
        void SpawnScene();
    }
}