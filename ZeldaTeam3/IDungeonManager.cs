using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public interface IDungeonManager : IDrawable
    {
        Point CurrentRoom { get; }

        void Transition(Direction roomDirection, bool unlock);
        void JumpToRoom(int row, int column, Direction facing = Direction.Up);
        void ResetScenes();
        void LoadScenes(IPlayer player);
        void LoadDungeonContent(ContentManager content);
    }
}
