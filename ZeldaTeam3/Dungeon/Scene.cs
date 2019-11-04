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
        private int _enemyCount = -1;

        public Scene(Room room, IPlayer player)
        {
            _room = room;
            _player = player;
            _room.player = _player;
            //_player.Projectiles = new List<IProjectile>();
           // _room.Projectiles = player.Projectiles;
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
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }

            List<IProjectile> projectileCollisions = new List<IProjectile>();

            foreach (var roomEnemy in _room.Enemies)
            {

                roomEnemy.Update();
                foreach(var projectile in roomEnemy.Projectiles)
                {
                    projectile.Update();
                   
                }

                //Get it to hit the barrier. If projectile.collidesWithEnemy -> halted = true;
                //If Projectile.CollidesWithPlayer -> halted = true;
                //If Projectile.CollidesWith Block -> halted = true;
                int k = 0;
                while (k < roomEnemy.Projectiles.Count)
                {
                    if (roomEnemy.Projectiles.ElementAt(k).Halted)
                    {
                        roomEnemy.Projectiles.RemoveAt(k);
                    }
                    else
                    {
                        projectileCollisions.Add(roomEnemy.Projectiles.ElementAt(k));
                        //if the projectile was still valid, then we'll need to check it's collisions
                    }
                    k++;
                }
                //Above loop checks all of the enemy projectiles, for each enemy

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
            }

            foreach (var roomCollidable in _room.Collidables)
            {
                if (roomCollidable.CollidesWith(_player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(_player).Execute();

                int j = 0;
 
                    while (j < _player.Projectiles.Count)
                    {
                        if (_player.Projectiles.ElementAt(j).Halted)
                        {
                            _player.Projectiles.RemoveAt(j);
                        }
                        else
                        {
                            projectileCollisions.Add(_player.Projectiles.ElementAt(j));
                        }
                        j++;
                    }

                
                //Above loop checks player for projectiles, and then determines if any are invalid

            }

 
            foreach (var projectile in projectileCollisions)
            {

                if (projectile.CollidesWith(_player.BodyCollision.Bounds))
                {
                    //projectile hit link
                    projectile.PlayerEffect(_player).Execute();
                }
                
                foreach(var enemy in _room.Enemies)
                {
                    if (projectile.CollidesWith(enemy.Bounds))
                    {
                        projectile.EnemyEffect(enemy).Execute();
                    }
                }

                foreach(var collidable in _room.Collidables)
                {
                    if (projectile.CollidesWith(collidable.Bounds))
                    {
                        collidable.ProjectileEffect(projectile).Execute();
                    }
                }
                

            }
                //Run the valid projectiles (that is, the ones that have not hit an enemy or player, or were Halted in the last call) and check collisions

            

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
