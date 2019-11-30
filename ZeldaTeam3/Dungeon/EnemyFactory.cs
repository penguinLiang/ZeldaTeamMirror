using System;
using Microsoft.Xna.Framework;
using Zelda.Enemies;

namespace Zelda.Dungeon
{
    public class EnemyFactory
    {
        public static IEnemy MakeEnemy(Point spawnPoint, EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Gel:
                    return new Gel(spawnPoint);
                case EnemyType.Goriya:
                    return new Goriya(spawnPoint);
                case EnemyType.Keese:
                    return new Keese(spawnPoint);
                case EnemyType.OldMan:
                    return new OldMan(spawnPoint);
                case EnemyType.Stalfos:
                    return new Stalfos(spawnPoint);
                case EnemyType.Trap:
                    return new Trap(spawnPoint);
                case EnemyType.WallMaster:
                    return new WallMaster(spawnPoint);
                case EnemyType.Aquamentus:
                    return new Aquamentus(spawnPoint);
                case EnemyType.Fygar:
                    return new Fygar(spawnPoint);
                case EnemyType.None:
                    throw new Exception("Room spawns an enemy but no type is set");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}