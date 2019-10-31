using System;
using Microsoft.Xna.Framework;

namespace Zelda.Dungeon
{
    public class TeleportLocation
    {
        public static Point Calculate(Direction playerFacing)
        {
            switch (playerFacing)
            {
                case Direction.Down:
                    return new Point(16 * 7 + 8, 16 * 2);
                case Direction.Up:
                    return new Point(16 * 7 + 8, 16 * 8);
                case Direction.Left:
                    return new Point(16 * 13,16 * 5);
                case Direction.Right:
                    return new Point(16 * 2,16 * 5);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
