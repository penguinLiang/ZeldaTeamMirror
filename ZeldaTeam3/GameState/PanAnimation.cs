using System;
using Microsoft.Xna.Framework;

namespace Zelda.GameState
{
    internal class PanAnimation : IUpdatable
    {
        private const int TransitionFrames = 60;
        private const int SceneWidth = 256;
        private const int SceneHeight = 176;
        private const int HorizontalSpeed = SceneWidth / TransitionFrames;
        private const int VerticalSpeed = SceneHeight / TransitionFrames;
        public Point SourceOffset { get; private set; }
        public Point DestinationOffset => SourceOffset + _directionOffset;
        public bool Finished => ReachedDestination();
        private readonly Point _directionOffset;
        private readonly Point _velocity;

        public Direction Direction { get; private set; }

        public PanAnimation(Direction direction)
        {
            Direction = direction;
            switch (direction)
            {
                case Direction.Up:
                    _velocity = new Point(0, VerticalSpeed);
                    _directionOffset = new Point(0, -SceneHeight);
                    break;
                case Direction.Down:
                    _velocity = new Point(0, -VerticalSpeed);
                    _directionOffset = new Point(0, SceneHeight);
                    break;
                case Direction.Left:
                    _velocity = new Point(HorizontalSpeed, 0);
                    _directionOffset = new Point(-SceneWidth, 0);
                    break;
                case Direction.Right:
                    _velocity = new Point(-HorizontalSpeed, 0);
                    _directionOffset = new Point(SceneWidth, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool ReachedDestination()
        {
            switch (Direction)
            {
                case Direction.Up:
                case Direction.Down:
                    return DestinationOffset.Y == 0;
                case Direction.Left:
                case Direction.Right:
                    return DestinationOffset.X == 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            if (Finished) return;
            SourceOffset += _velocity;
        }
    }
}
