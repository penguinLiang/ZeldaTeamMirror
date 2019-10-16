using System;
using Microsoft.Xna.Framework;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable, IScene
    {
        private readonly SceneController _controller;
        private readonly Room _room;
        private readonly IPlayer _player;
        private int _enemyCount = -1;

        private ISprite _background = BackgroundSpriteFactory.Instance.CreateDungeonBackground();

        public Scene(SceneController controller, Room room, IPlayer player)
        {
            _controller = controller;
            _room = room;

            if (_enemyCount == -1) _enemyCount = _room.Enemies.Count;
            for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }
        }

        public void TransitionToRoom(int row, int column)
        {
            _controller.TransitionToRoom(row, column);
        }

        public void Update()
        {
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }

            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Update();

                foreach (var roomCollidable in _room.Collidables)
                {
                    /* Enemy bounds not implemented
                    if (roomCollidable.CollidesWith(roomEnemy.Bounds))
                        roomCollidable.EnemyEffect(roomEnemy);
                    */
                }

                /* Player collision not implemented
                    if (_player.CollidesWith(roomEnemy.Bounds))
                        _player.EnemyEffect(roomEnemy);

                    if (roomEnemy.CollidesWith(_player.Bounds))
                        roomEnemy.PlayerEffect(roomEnemy);
                */

                if (!roomEnemy.Alive)
                {
                    _enemyCount--;
                }
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                /* Player collision not implemented
                    if (roomCollidable.CollidesWith(roomEnemy.Bounds))
                        _player.PlayerEffect(roomEnemy);
                */
            }
        }

        public void Draw()
        {
            _background.Draw(Vector2.Zero);

            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Draw();
            }

            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Draw();
            }
        }
    }

    public class SceneController
    {
        public void TransitionToRoom(int row, int column)
        {
            Console.WriteLine("Dummy called! Row is " + row + ". Column is " + column + ".");
        }
    }
}
