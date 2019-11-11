using System;

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
    }
}
