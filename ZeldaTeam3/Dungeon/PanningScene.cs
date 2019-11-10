using Microsoft.Xna.Framework;

namespace Zelda.Dungeon
{
    public class PanningScene : IDrawable
    {
        private readonly Room _room;
        private readonly ISprite _background;

        public PanningScene(Room room, ISprite background)
        {
            _room = room;
            _background = background;
            room.TransitionReset();
        }

        public void Update()
        {
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }
        }

        public void Draw()
        {
            _background?.Draw(Vector2.Zero);

            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Draw();
            }
        }
    }
}
