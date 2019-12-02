using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zelda.Projectiles;

namespace Zelda.Survival
{
    class EnemyTargetManager
    {
        public static Point GetTargetLocation(List<IProjectile> projectiles, Point playerLocation, Point enemyLocation)
        {
            Point targetLocation = playerLocation;
            List<Point> baitLocations = new List<Point>();

            foreach (var projectile in projectiles)
            {
                if (projectile is Bait)
                    baitLocations.Add(projectile.Bounds.Location);
            }

            if (baitLocations.Count > 0)
            {
                targetLocation = baitLocations[0];
                double closestDistanceSquared = Math.Pow(enemyLocation.X - targetLocation.X, 2) + Math.Pow(enemyLocation.Y - targetLocation.Y, 2);
                for (int i = 1; i < baitLocations.Count; i++)
                {
                    if (Math.Pow(enemyLocation.X - baitLocations[i].X, 2) + Math.Pow(enemyLocation.Y - baitLocations[i].Y, 2) < closestDistanceSquared)
                    {
                        targetLocation = baitLocations[i];
                        closestDistanceSquared = Math.Pow(enemyLocation.X - targetLocation.X, 2) + Math.Pow(enemyLocation.Y - targetLocation.Y, 2);
                    }
                }
            }

            return targetLocation;
        }
    }
}
