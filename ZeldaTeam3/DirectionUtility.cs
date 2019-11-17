using System;
using Microsoft.Xna.Framework;

namespace Zelda
{
    internal class DirectionUtility
    {
        private const int NumDirections = 4;
        private static readonly Random Rng = new Random();

        public static Direction Flip(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Direction RotateClockwise(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Down;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Direction RandomDirection()
        {
            return (Direction) Rng.Next(NumDirections);
        }

        public static Direction RandomVerticalDirection()
        {
            var direction = RandomDirection();
            // ReSharper disable once SwitchStatementMissingSomeCases (that's what the default is for)
            switch (direction)
            {
                case Direction.Left:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Down;
                default:
                    return direction;
            }
        }

        public static Direction GetDirectionTowardsPoint(Point currentPoint, Point destinationPoint)
        {
            var xDiff = destinationPoint.X - currentPoint.X;
            var yDiff = destinationPoint.Y - currentPoint.Y;

            if (Math.Abs(xDiff) > Math.Abs(yDiff))
            {
                return xDiff > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                return yDiff > 0 ? Direction.Down : Direction.Up;
            }
        }
    }
}
