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
        //put an IProjectile list here? Check if it's halted here

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
            foreach (var roomDrawable in _room.Drawables)
            {
                roomDrawable.Update();
            }
    
            
            foreach (var roomEnemy in _room.Enemies)
            {
                roomEnemy.Update();
                //For each roomEnemy.projectiles
                //How to remove item if you're iterating over them
                //projectile.Update();

                foreach(var projectile in roomEnemy.Projectiles)
                {
                    projectile.Update();
                }

                foreach(var projectile in roomEnemy.Projectiles)
                {
                    //see if it is halted
                    //if halted, remove from array
                    if (projectile.Halted)
                    {
                        //remove entirely
                    }
                }

              

                //if projectile.isHalted -> remove from array
                //check each item in projectile array, see if it needs to be removed
                //Removal code?
                //add each projectile to a list, then do a collision check
                //As you update, loop though and remove projectiles that aren't active anymore, so loop through again, make a local array of projectiles
              
                //Concatenate the remaining arrays -> Then check all collisions at once

                //Do collision detection of each enemy projectile
                //Projectile.CollidesWith(IEnemy)
                //Projectile.CollidesWith(Player)
                
                    ///SCENE Handles removal, link/enemy do not care after they throw it out there


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
           //check projectiles to see if they collide with wall
                
                //foreach player . . .

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
                //foreach Projectile in Enemy
                //draw
            }
        }
    }
}
