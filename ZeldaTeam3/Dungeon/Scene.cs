using System.Collections.Generic;
using System.Linq;
using Zelda.Blocks;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable
    {
        private const int ThrottleFrameDuration = 50;
        private readonly Room _room;
        private readonly IPlayer _player;
        private readonly Dictionary<IEnemy, int> _enemiesAttackThrottle = new Dictionary<IEnemy, int>();
        private int _enemyCount = -1;
        private int _roomRow;
        private int _roomCol;

        public Scene(int row, int col, Room room, IPlayer player)
        {
            _room = room;
            _player = player;
            _roomRow = row;
            _roomCol = col;
        }

        public void Reset()
        {
            _enemyCount = _room.Enemies.Count;
        }

        public void SpawnEnemies()
        {
            if (_enemyCount == -1)
            {
                _enemyCount = _room.Enemies.Count;
            }

            if(_roomRow == 2 && _roomCol == 1) 
            {
                _room.MoveableBlockReset();
            }
            for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }
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
                    if (!roomCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    roomCollidable.EnemyEffect(roomEnemy).Execute();
                }

                if (_player.Alive && _player.UsingPrimaryItem && !_enemiesAttackThrottle.ContainsKey(roomEnemy) && _player.SwordCollision.CollidesWith(roomEnemy.Bounds))
                {
                    _player.SwordCollision.EnemyEffect(roomEnemy).Execute();
                    if (!roomEnemy.Alive)
                    {
                        _enemyCount--;
                        if (roomEnemy is Enemies.Stalfos || roomEnemy is Enemies.Goriya || roomEnemy is Enemies.WallMaster)
                            _room.AddDroppedItem(roomEnemy.Bounds.X, roomEnemy.Bounds.Y);
                    }
                    _enemiesAttackThrottle[roomEnemy] = ThrottleFrameDuration;
                }

                if (roomEnemy.Alive && roomEnemy.CollidesWith(_player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(_player).Execute();
                }
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                if (roomCollidable.CollidesWith(_player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(_player).Execute();
            }

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (LINQ is slow here)
            foreach (var key in _enemiesAttackThrottle.Keys.ToList())
            {
                if (_enemiesAttackThrottle[key]-- == 0) _enemiesAttackThrottle.Remove(key);
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
