using Microsoft.Xna.Framework;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable, IScene
    {
        private readonly DungeonManager _manager;
        private readonly Room _room;
        private readonly IPlayer _player;
        private int _enemyCount = -1;

        public Scene(DungeonManager manager, Room room, IPlayer player)
        {
            _manager = manager;
            _room = room;
            _player = player;
        }

        public void SpawnEnemies()
        {
            if (_enemyCount == -1) _enemyCount = _room.Enemies.Count;
            for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }
        }

        public void TransitionToRoom(int row, int column)
        {
            _manager.TransitionToRoom(row, column);
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
                    if (roomCollidable.CollidesWith(roomEnemy.Bounds))
                    {
                        roomCollidable.EnemyEffect(roomEnemy).Execute();
                    }

                }

                if (_player.Alive && _player.UsingPrimaryItem && _player.SwordCollision.CollidesWith(roomEnemy.Bounds))
                {
                    _player.SwordCollision.EnemyEffect(roomEnemy).Execute();
                }

                if (roomEnemy.Alive && roomEnemy.CollidesWith(_player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(_player).Execute();
                }

                if (!roomEnemy.Alive)
                {
                    _enemyCount--;
                }
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                if (roomCollidable.CollidesWith(_player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(_player).Execute();
            }
        }

        public void Draw()
        {
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
}
