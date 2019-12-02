using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Zelda.Enemies
{
    public class Gel : EnemyAgent
    {
        private ISprite _sprite;
        protected override ISprite Sprite => _sprite;
        private static readonly Point Size = new Point(12, 12);
        public override Rectangle Bounds => new Rectangle(Location, Alive ? Size : Point.Zero);
        private static readonly List<AgentState> ValidAgentStates = new List<AgentState>
        {
            AgentState.Ready,
            AgentState.Moving,
            AgentState.Halted
        };

        private readonly Point _origin;

        private Direction _currentDirection;
        private AgentState _agentStatus;

        private const int ActionDelay = 16;
        private int _agentClock;

        public Gel(Point location)
        {
            _origin = location;
            _agentStatus = AgentState.Ready;
        }

        public override void Spawn()
        {
            base.Spawn();

            _sprite = EnemySpriteFactory.Instance.CreateGel();
            Location = _origin;
            _currentDirection = Direction.Down;
        }

        protected override void Move(Direction direction, int speed = 1)
        {
            var initialSpeed = Speed;
            Speed = speed;
            switch (direction)
            {
                case Direction.Up:
                    Location.Y -= Speed;
                    break;
                case Direction.Down:
                    Location.Y += Speed;
                    break;
                case Direction.Left:
                    Location.X -= Speed;
                    break;
                case Direction.Right:
                    Location.X += Speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Speed = initialSpeed;
        }

        private void FlipDirection()
        {
            _currentDirection = DirectionUtility.Flip(_currentDirection);
        }

        public void ExecuteAction()
        {
            if (_agentClock > 0)
            {
                _agentClock--;
            }

            switch (_agentStatus)
            {
                case AgentState.Ready: //determine next action
                    UpdateAction();
                    break;
                case AgentState.Stunned:
                case AgentState.Halted:
                    if (_agentClock == 0)
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Knocked:
                    if (_agentClock == 0)
                    {
                        Speed = 1;
                        _agentStatus = AgentState.Ready;
                    }
                    else
                    {
                        Move(DirectionUtility.Flip(_currentDirection));
                    }

                    break;
                case AgentState.Moving:
                    if (_agentClock != 0)
                    {
                        Move(_currentDirection);
                    }
                    else
                    {
                        _agentStatus = AgentState.Ready;
                    }

                    break;
                case AgentState.Attacking:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void UpdateAction()
        {
            _agentStatus = AgentStateUtility.RandomFrom(ValidAgentStates);
            if (_agentStatus == AgentState.Moving)
            {
                _currentDirection = DirectionUtility.RandomDirection();
            }
            _agentClock = ActionDelay;
        }

        protected override void Knockback()
        {
            _agentStatus = AgentState.Knocked;
            Speed = 2;
            _agentClock = ActionDelay;
            FlipDirection();
        }

        public override void Halt()
        {
            _agentStatus = AgentState.Halted;
            _agentClock = ActionDelay*3;
            FlipDirection();
            Move(_currentDirection);
        }

        public override void Stun()
        {
            _agentClock = 240;
            _agentStatus = AgentState.Stunned;
        }

        public override void Update()
        {
            base.Update();

            if (Alive && CanMove)
                ExecuteAction();
        }
    }
}