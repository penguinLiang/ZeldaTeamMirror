using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Survival
{
    internal class ZoneManager
    {
        private static readonly Point TileSize = new Point(Dimensions.TileSize, Dimensions.TileSize);

        private static readonly Rectangle[] TileZones =
        {
            new Rectangle( new Point(0, 0) * TileSize, new Point(16, 11) * TileSize),
            new Rectangle(new Point(16, 0) * TileSize, new Point(16, 22) * TileSize),
            new Rectangle(new Point(32, 0) * TileSize, new Point(16, 11) * TileSize),
            new Rectangle(new Point(0, 11) * TileSize, new Point(16, 22) * TileSize),
            new Rectangle(new Point(16, 22) * TileSize, new Point(16, 11) * TileSize),
            new Rectangle(new Point(32, 11) * TileSize, new Point(16, 22) * TileSize)
        };

        private static Rectangle ZoneContaining(Point location)
        {
            foreach (var tileZone in TileZones)
            {
                if (tileZone.Contains(location)) return tileZone;
            }
            return new Rectangle(-1, -1, 0, 0);
        }

        public static List<Point> SpawnTilesWithinZone(List<Point> spawnTiles, Point location)
        {
            var zone = ZoneContaining(location);
            return spawnTiles.FindAll(spawnTile => zone.Contains(spawnTile));
        }
    }
}
