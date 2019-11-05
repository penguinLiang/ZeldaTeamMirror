using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Items;

namespace Zelda.Dungeon
{
    public class Scene : IDrawable
    {
        private const int ThrottleFrameDuration = 50;
        private readonly Room _room;
        private readonly IPlayer _player;
        private readonly Dictionary<IEnemy, int> _enemiesAttackThrottle = new Dictionary<IEnemy, int>();
        private readonly List<IProjectile> _projectiles = new List<IProjectile>();
        private readonly List<IItem> _droppedItems = new List<IItem>();
        private readonly int _roomRow;
        private readonly int _roomCol;
        private readonly Random _rnd = new Random();
        private int _enemyCount = -1;

        public Scene(int row, int col, Room room, IPlayer player)
        {
            _room = room;
            _player = player;
            _roomRow = row;
            _roomCol = col;
            _room.Collidables.AddRange(_room.Items);
            _room.Drawables.AddRange(_room.Items);
        }

        public void Reset()
        {
            _enemyCount = _room.Enemies.Count;
            foreach (var roomItem in _room.Items)
            {
                roomItem.Reset();
            }
            _droppedItems.Clear();
        }

        public void SpawnScene()
        {
            _droppedItems.Clear();

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

        private void TryDestroyEnemy(IEnemy roomEnemy)
        {
            if (roomEnemy.Alive) return;

            _enemyCount--;
            if (roomEnemy is Enemies.Stalfos || roomEnemy is Enemies.Goriya || roomEnemy is Enemies.WallMaster)
                AddDroppedItem(roomEnemy.Bounds.X, roomEnemy.Bounds.Y);
        }

        private void AddDroppedItem(int enemyX, int enemyY)
        {
            var rand = _rnd.Next(100);
            if (rand < 50) return; // No drop = 50%

            IItem item = new Rupee(new Point(enemyX, enemyY)); // 1 Rupee = 10%

            if (rand < 60)
            {
                item = new DroppedHeart(new Point(enemyX, enemyY)); // Dropped Heart = 10%
            }
            else if (rand < 70)
            {
                item = new Rupee5(new Point(enemyX, enemyY)); // 5 Rupee = 10%
            }
            else if (rand < 80)
            {
                item = new BombItem(new Point(enemyX, enemyY)); // Bomb = 10%
            }
            else if (rand > 90)
            {
                item = new Fairy(new Point(enemyX, enemyY)); // Fairy = 10%
            }

            _droppedItems.Add(item);
        }


        public void Update()
        {
            for (var i = 0; i < _projectiles.Count; i++)
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

            foreach (var droppedItem in _droppedItems)
            {
                droppedItem.Update();
                if (droppedItem.CollidesWith(_player.BodyCollision.Bounds))
                {
                    droppedItem.PlayerEffect(_player).Execute();
                }
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
                    TryDestroyEnemy(roomEnemy);
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
                        TryDestroyEnemy(roomEnemy);
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

            foreach (var droppedItem in _droppedItems)
            {
                droppedItem.Draw();
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
