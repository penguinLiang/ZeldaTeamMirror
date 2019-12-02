using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Blocks;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Projectiles;

// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
namespace Zelda.Survival
{
    public class SurvivalScene : IScene
    {
        private const int ThrottleFrameDuration = 50;
        private readonly SurvivalRoom _room;
        private readonly WaveManager _waveManager;
        private readonly IPlayer _player;
        private readonly Dictionary<IEnemy, int> _enemiesAttackThrottle = new Dictionary<IEnemy, int>();
        private readonly List<IProjectile> _projectiles = new List<IProjectile>();
        private readonly List<IItem> _items = new List<IItem>();
        private readonly Random _rnd = new Random((int) DateTime.Now.Ticks);
        private int _enemyCount = int.MinValue;

        public SurvivalScene(SurvivalRoom room, WaveManager waveManager, IPlayer player)
        {
            _room = room;
            _waveManager = waveManager;
            _player = player;
            _items.AddRange(_room.Items);
            foreach (var item in _items)
            {
                item.Reset();
            }

            foreach(var barricade in _room.Barricade)
            {
                barricade.Reset();
            }

            foreach (var roomDoor in _room.Doors.Values)
            {
                roomDoor.Reset();
            }
        }

        public void DestroyProjectiles()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Halt();
            }
            _projectiles.Clear();
        }

        public void SpawnScene()
        {
            _projectiles.Clear();

            if (_enemyCount == int.MinValue)
            {
                _enemyCount = _waveManager.Enemies.Count;
            }

            foreach (var roomDoor in _room.Doors.Values)
            {
                roomDoor.Deactivate();
            }
            _room.TransitionReset();
        }

        private void PlayerAttackCollision(ICollideable collision, IEnemy roomEnemy)
        {
            if (!_player.Alive || _enemiesAttackThrottle.ContainsKey(roomEnemy) || !collision.CollidesWith(roomEnemy.Bounds)) return;
            collision.EnemyEffect(roomEnemy).Execute();
            _enemiesAttackThrottle[roomEnemy] = ThrottleFrameDuration;

            if (roomEnemy.Alive) return;

            if (roomEnemy is Stalfos || roomEnemy is Goriya || roomEnemy is WallMaster)
                AddDroppedItem(roomEnemy.Bounds.Location);
        }

        private void AddDroppedItem(Point location)
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

            _items.Add(item);
        }

        private Point GetEnemyTargetLocation(Point enemyLocation)
        {
            Point targetLocation = _player.Location;
            List<Point> baitLocations = new List<Point>();

            foreach (var projectile in _projectiles)
            {
                if (projectile is Bait)
                    baitLocations.Add(projectile.Bounds.Location);
            }

            if (baitLocations.Count > 0)
            {
                targetLocation = baitLocations[0];
                double closestDistanceSquared = Math.Pow(_player.Location.X - targetLocation.X, 2) + Math.Pow(_player.Location.Y - targetLocation.Y, 2);
                for (int i = 1; i < baitLocations.Count; i++)
                {
                    if (Math.Pow(_player.Location.X - baitLocations[i].X, 2) + Math.Pow(_player.Location.Y - baitLocations[i].Y, 2) < closestDistanceSquared)
                    {
                        targetLocation = baitLocations[i];
                        closestDistanceSquared = Math.Pow(_player.Location.X - targetLocation.X, 2) + Math.Pow(_player.Location.Y - targetLocation.Y, 2);
                    }
                }
            }

            return targetLocation;
        }

        public void Update()
        {
            var prioritizedCoinCollisions = new List<Rectangle>();
            
            for (var i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].Update();
                if (!_projectiles[i].Halted) continue;

                if (_projectiles[i] is SwordBeam)
                {
                    _projectiles[i] = new SwordBeamParticles(_projectiles[i].Bounds.Location);
                }
                else if (_projectiles[i] is LaunchedBomb)
                {
                    _projectiles[i] = new LaunchedBombExplosion(_projectiles[i].Bounds.Location);
                }
                else
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

            foreach (var droppedItem in _items)
            {
                droppedItem.Update();
                if (droppedItem.CollidesWith(_player.BodyCollision.Bounds))
                {
                    droppedItem.PlayerEffect(_player).Execute();
                }
            }

            foreach(var barricade in _room.Barricade)
            {
                barricade.Update();
            }

            foreach (var roomEnemy in _waveManager.Enemies)
            {
                roomEnemy.Target(GetEnemyTargetLocation(roomEnemy.Bounds.Location));
                roomEnemy.Update();
                _projectiles.AddRange(roomEnemy.Projectiles);
                roomEnemy.Projectiles.Clear();

                foreach (var roomCollidable in _room.Collidables)
                {
                    if (!roomCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    roomCollidable.EnemyEffect(roomEnemy).Execute();
                }

                foreach(var barricadeCollidable in _room.Barricade)
                {
                    if (!barricadeCollidable.CollidesWith(roomEnemy.Bounds)) continue;

                    barricadeCollidable.EnemyEffect(roomEnemy).Execute();
                }

                PlayerAttackCollision(_player.UsingPrimaryItem ? _player.SwordCollision : _player.BodyCollision, roomEnemy);

                if (roomEnemy.Alive && roomEnemy.CollidesWith(_player.BodyCollision.Bounds))
                {
                    roomEnemy.PlayerEffect(_player).Execute();
                }

                foreach (var projectile in _projectiles)
                {
                    if (roomEnemy.CollidesWith(projectile.Bounds))
                    {
                        roomEnemy.ProjectileEffect(projectile).Execute();
                        if (projectile is AlchemyCoin)
                            prioritizedCoinCollisions.Add(roomEnemy.Bounds);
                    }

                    if (projectile is ClockCollideable)
                        projectile.EnemyEffect(roomEnemy).Execute();
                    else
                        PlayerAttackCollision(projectile, roomEnemy);
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
                    if (projectile is AlchemyCoin)
                        prioritizedCoinCollisions.Insert(0, roomCollidable.Bounds);

                    foreach(var barricade in _room.Barricade)
                    {
                        if (projectile.CollidesWith(barricade.Bounds))
                        {
                            barricade.ProjectileEffect(projectile).Execute();
                        }
                    }
                }
            }

            foreach(var barricade in _room.Barricade)
            {
                if (barricade.CollidesWith(_player.BodyCollision.Bounds))
                    barricade.PlayerEffect(_player).Execute();

                foreach(var otherBarricade in _room.Barricade)
                {
                    if (barricade.CollidesWith(otherBarricade.Bounds)&& barricade is KeyBarrierCenter)
                    {
                        if (otherBarricade is KeyBarrier && barricade.unlocked)
                        {
                            otherBarricade.Unlock();
                        }
                    }

                    if (!barricade.CollidesWith(otherBarricade.Bounds) || !(barricade is RupeeBarrierCenter)) continue;

                    if (otherBarricade.GetType() == typeof(RupeeBarrier)&& barricade.unlocked)
                        otherBarricade.Unlock();
                }
            }

            foreach (var projectile in _projectiles)
            {
                if (projectile is AlchemyCoin coin)
                {
                    coin.Reflect(prioritizedCoinCollisions);
                }

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

            foreach (var droppedItem in _items)
            {
                droppedItem.Draw();
            }

            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }

            foreach (var roomEnemy in _waveManager.Enemies)
            {
                roomEnemy.Draw();
            }

            foreach (var barricade in _room.Barricade)
            {
                barricade.Draw();
            }
        }
    }
}
