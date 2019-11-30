using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Projectiles;

// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
namespace Zelda.Dungeon
{
    public class Scene : IDrawable
    {
        private const int ThrottleFrameDuration = 50;
        protected readonly Room Room;
        protected readonly IPlayer Player;
        protected readonly Dictionary<IEnemy, int> EnemiesAttackThrottle = new Dictionary<IEnemy, int>();
        protected readonly List<IProjectile> Projectiles = new List<IProjectile>();
        protected readonly List<IItem> Items = new List<IItem>();
        private readonly Random _rnd = new Random((int) DateTime.Now.Ticks);
        protected int EnemyCount = int.MinValue;

        public Scene(Room room, IPlayer player)
        {
            Room = room;
            Player = player;
            Items.AddRange(Room.Items);
            foreach (var item in Items)
            {
                item.Reset();
            }

            foreach (var roomDoor in Room.Doors.Values)
            {
                roomDoor.Reset();
            }
        }

        public void DestroyProjectiles()
        {
            foreach (var projectile in Projectiles)
            {
                projectile.Halt();
            }
            Projectiles.Clear();
        }

        public virtual void SpawnScene()
        {
            Projectiles.Clear();

            if (EnemyCount == int.MinValue)
            {
                EnemyCount = Room.Enemies.Count;
            }

            foreach (var roomDoor in Room.Doors.Values)
            {
                roomDoor.Deactivate();
            }
            Room.TransitionReset();

            for (var i = 0; i < EnemyCount; i++)
            {
                Room.Enemies[i].Spawn();
            }
        }

        protected void PlayerAttackCollision(ICollideable collision, IEnemy roomEnemy)
        {
            if (!Player.Alive || EnemiesAttackThrottle.ContainsKey(roomEnemy) || !collision.CollidesWith(roomEnemy.Bounds)) return;
            collision.EnemyEffect(roomEnemy).Execute();
            EnemiesAttackThrottle[roomEnemy] = ThrottleFrameDuration;

            if (roomEnemy.Alive) return;
            if (--EnemyCount == 0)
            {
                foreach (var door in Room.Doors.Values)
                {
                    door.Activate();
                }
            }
            if (roomEnemy is Stalfos || roomEnemy is Goriya || roomEnemy is WallMaster)
                AddDroppedItem(roomEnemy.Bounds.Location);
        }

        protected virtual void AddDroppedItem(Point location)
        {
            var rand = _rnd.Next(100);
            if (rand < 50) return; // No drop = 50%

            IItem item;
            rand = _rnd.Next(5);

            switch (rand)
            {
                case 0:
                    item = new Rupee(location); // 1 Rupee = 10%
                    break;
                case 1:
                    item = new DroppedHeart(location); // Dropped Heart = 10%
                    break;
                case 2:
                    item = new Rupee5(location); // 5 Rupee = 10%
                    break;
                case 3:
                    item = new BombItem(location); // Bomb = 10%
                    break;
                default:
                    item = new Fairy(location); // Fairy = 10%
                    break;
            }

            Items.Add(item);
        }

        public void Update()
        {
            var prioritizedCoinCollisions = new List<Rectangle>();

            for (var i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update();
                if (!Projectiles[i].Halted) continue;

                if (Projectiles[i] is SwordBeam)
                {
                    Projectiles[i] = new SwordBeamParticles(Projectiles[i].Bounds.Location);
                }
                else if (Projectiles[i] is LaunchedBomb)
                {
                    Projectiles[i] = new LaunchedBombExplosion(Projectiles[i].Bounds.Location);
                }
                else
                {
                    Projectiles.RemoveAt(i--);
                }
            }

            Projectiles.AddRange(Player.Projectiles);
            Player.Projectiles.Clear();

            foreach (var roomDrawable in Room.Drawables)
            {
                roomDrawable.Update();
            }

            foreach (var droppedItem in Items)
            {
                droppedItem.Update();
                if (droppedItem.CollidesWith(Player.BodyCollision.Bounds))
                {
                    droppedItem.PlayerEffect(Player).Execute();
                }
            }

            foreach (var roomEnemy in Room.Enemies)
            {
                roomEnemy.Target(Player.Location);
                roomEnemy.Update();
                Projectiles.AddRange(roomEnemy.Projectiles);
                roomEnemy.Projectiles.Clear();

                foreach (var roomCollidable in Room.Collidables)
                {
                    if (!roomCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    roomCollidable.EnemyEffect(roomEnemy).Execute();
                }

                PlayerAttackCollision(Player.UsingPrimaryItem ? Player.SwordCollision : Player.BodyCollision, roomEnemy);

                if (roomEnemy.Alive && roomEnemy.CollidesWith(Player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(Player).Execute();
                }

                foreach (var projectile in Projectiles)
                {
                    if (roomEnemy.CollidesWith(projectile.Bounds))
                    {
                        roomEnemy.ProjectileEffect(projectile).Execute();
                        if (projectile is AlchemyCoin)
                            prioritizedCoinCollisions.Add(roomEnemy.Bounds);
                    }

                    PlayerAttackCollision(projectile, roomEnemy);
                }
            }

            foreach (var roomCollidable in Room.Collidables)
            {
                if (roomCollidable.CollidesWith(Player.BodyCollision.Bounds))
                    roomCollidable.PlayerEffect(Player).Execute();

                foreach (var projectile in Projectiles)
                {
                    if (!roomCollidable.CollidesWith(projectile.Bounds)) continue;

                    roomCollidable.ProjectileEffect(projectile).Execute();
                    if (projectile is AlchemyCoin)
                        prioritizedCoinCollisions.Insert(0, roomCollidable.Bounds);
                }
            }

            foreach (var projectile in Projectiles)
            {
                if (projectile is AlchemyCoin coin)
                {
                    coin.Reflect(prioritizedCoinCollisions);
                }

                if (projectile.CollidesWith(Player.BodyCollision.Bounds))
                {
                    projectile.PlayerEffect(Player).Execute();
                }

                if (Player.BodyCollision.CollidesWith(projectile.Bounds))
                {
                    Player.BodyCollision.ProjectileEffect(projectile).Execute();
                }
            }
 
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (LINQ is slow here)
            foreach (var key in EnemiesAttackThrottle.Keys.ToList())
            {
                if (EnemiesAttackThrottle[key]-- <= 0) EnemiesAttackThrottle.Remove(key);
            }
        }

        public void Draw()
        {
            foreach (var roomDrawable in Room.Drawables)
            {
                roomDrawable.Draw();
            }

            foreach (var droppedItem in Items)
            {
                droppedItem.Draw();
            }

            foreach (var projectile in Projectiles)
            {
                projectile.Draw();
            }

            foreach (var roomEnemy in Room.Enemies)
            {
                roomEnemy.Draw();
            }
        }

        public void ResetEnemies()
        {
            EnemyCount = int.MinValue;
        }
    }
}
