using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Projectiles;

// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator (this is never helpful)
namespace Zelda.Survival
{
    public sealed class SurvivalScene : Scene
    {
        private readonly Random _rnd = new Random((int) DateTime.Now.Ticks);
        private SurvivalRoom _survivalRoom;

        public SurvivalScene(SurvivalRoom room, IPlayer player): base(room, player)
        {
            _survivalRoom = room;
        }

        public void SpawnEnemy(int enemyId) => _survivalRoom.SpawnEnemy(enemyId);


        // Change 
        public override void SpawnScene()
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

            /*for (var i = 0; i < _enemyCount; i++)
            {
                _room.Enemies[i].Spawn();
            }*/
        }

        protected override void AddDroppedItem(Point location)
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
    }
}
