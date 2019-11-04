using System.Collections.Generic;
using System.Linq;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable
    {
        private const int ThrottleFrameDuration = 50;
        private readonly Room _room;
        private readonly IPlayer _player;
        private readonly Dictionary<IEnemy, int> _enemiesAttackThrottle = new Dictionary<IEnemy, int>();
        private readonly List<IProjectile> _projectiles = new List<IProjectile>();
        private int _enemyCount = -1;

        public Scene(Room room, IPlayer player)
        {
            _room = room;
            _player = player;
        }

        public void Reset()
        {
            _enemyCount = _room.Enemies.Count;
        }

        public void SpawnEnemies()
        {
            if (_enemyCount == -1) _enemyCount = _room.Enemies.Count;
            for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }
        }

        public void Update()
        {
            int initialCount = _projectiles.Count;
            for (var i = 0; i < initialCount; i++)
            {
                _projectiles[i].Update();
                if (_projectiles[i].Halted)
                {
                    _projectiles.RemoveAt(i--);
                }
            }

            _projectiles.AddRange(_player.Projectiles);
            _player.Projectiles.Clear();

            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }

            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Update();
                _projectiles.AddRange(roomEnemy.Projectiles);
                roomEnemy.Projectiles.Clear();

                foreach (var roomCollidable in _room.Collidables)
                {
                    if (!roomCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    roomCollidable.EnemyEffect(roomEnemy).Execute();
                }

                if (_player.Alive && _player.UsingPrimaryItem && !_enemiesAttackThrottle.ContainsKey(roomEnemy) && _player.SwordCollision.CollidesWith(roomEnemy.Bounds))
                {
                    _player.SwordCollision.EnemyEffect(roomEnemy).Execute();
                    if (!roomEnemy.Alive) _enemyCount--;
                    _enemiesAttackThrottle[roomEnemy] = ThrottleFrameDuration;
                }

                if (roomEnemy.Alive && roomEnemy.CollidesWith(_player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(_player).Execute();
                }

                foreach (var projectile in _projectiles)
                {
                    if (roomEnemy.CollidesWith(projectile.Bounds))
                    {
                        roomEnemy.ProjectileEffect(projectile).Execute();
                    }

                    if (projectile.CollidesWith(roomEnemy.Bounds))
                    {
                        projectile.EnemyEffect(roomEnemy).Execute();
                    }
                }
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                if (roomCollidable.CollidesWith(_player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(_player).Execute();

                foreach (var projectile in _projectiles)
                {
                    if (!roomCollidable.CollidesWith(projectile.Bounds)) continue;

                    roomCollidable.ProjectileEffect(projectile).Execute();
                }
            }

            foreach (var projectile in _projectiles)
            {
                if (projectile.CollidesWith(_player.BodyCollision.Bounds))
                {
                    projectile.PlayerEffect(_player).Execute();
                }

                if (_player.BodyCollision.CollidesWith(projectile.Bounds))
                {
                    _player.BodyCollision.ProjectileEffect(projectile).Execute();
                }
            }
 
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (LINQ is slow here)
            foreach (var key in _enemiesAttackThrottle.Keys.ToList())
            {
                if (_enemiesAttackThrottle[key]-- <= 0) _enemiesAttackThrottle.Remove(key);
            }
        }

        public void Draw()
        {
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Draw();
            }

            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }

            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Draw();
            }
        }
    }
}
